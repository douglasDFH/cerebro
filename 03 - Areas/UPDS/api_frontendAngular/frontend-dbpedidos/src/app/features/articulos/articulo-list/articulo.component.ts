import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ArticuloDto, ArticuloService } from '../../../core/services/articulo.service';
import { CategoriaService, CategoriaDto } from '../../../core/services/categoria.service';
import { PresentacionService, PresentacionDto } from '../../../core/services/presentacion.service';
import Swal from 'sweetalert2';

// Importaciones de Angular Material
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-articulo',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTableModule,
    MatCardModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatToolbarModule,
    MatDialogModule,
    MatSnackBarModule,
  ],
  encapsulation: ViewEncapsulation.None,
  templateUrl: './articulo.component.html',
  styleUrls: ['./articulo.component.scss']
})
export class ArticuloComponent implements OnInit {
  articulos: ArticuloDto[] = [];
  categorias: CategoriaDto[] = [];
  presentaciones: PresentacionDto[] = [];

  cargando = true;
  error = '';
  form!: FormGroup;
  editandoId: number | null = null;
  imagenBase64: string | null = null;
  imagenPreview: string | null = null;

  filtro = '';
  paginaActual = 1;
  registrosPorPagina = 5;
  opcionesPorPagina = [5, 10, 25, 50];

  mostrarFormulario = false;
  ordenColumna: string = 'idArticulo';
  ordenAscendente: boolean = true;
  modoTarjeta = true;

  constructor(
    private articuloService: ArticuloService,
    private categoriaService: CategoriaService,
    private presentacionService: PresentacionService,
    private fb: FormBuilder
  ) {}

  async ngOnInit() {
    this.form = this.fb.group({
      codigo: ['', Validators.required],
      nombre: ['', Validators.required],
      descripcion: [''],
      idCategoria: [null, Validators.required],
      idPresentacion: [null, Validators.required],
    });

    await Promise.all([
      this.cargarCategorias(),
      this.cargarPresentaciones(),
      this.cargarArticulos()
    ]);
  }

  async cargarArticulos() {
    this.cargando = true;
    try {
      this.articulos = await this.articuloService.getAll();
    } catch {
      this.error = 'Error al cargar los artículos';
    } finally {
      this.cargando = false;
    }
  }

  async cargarCategorias() {
    this.categorias = await this.categoriaService.getAll();
  }

  async cargarPresentaciones() {
    this.presentaciones = await this.presentacionService.getAll();
  }

  onImageSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files?.length) {
      const file = input.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        this.imagenBase64 = (reader.result as string).split(',')[1];
        this.imagenPreview = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  get articulosFiltradosOrdenados() {
    const filtroLower = this.filtro.toLowerCase();
    const filtrados = this.articulos.filter(a =>
      a.nombre.toLowerCase().includes(filtroLower) ||
      a.codigo.toLowerCase().includes(filtroLower) ||
      a.descripcion?.toLowerCase().includes(filtroLower) ||
      a.categoriaNombre?.toLowerCase().includes(filtroLower) ||
      a.presentacionNombre?.toLowerCase().includes(filtroLower)
    );

    return filtrados.sort((a, b) => {
      const propA = (a as any)[this.ordenColumna];
      const propB = (b as any)[this.ordenColumna];

      if (propA < propB) return this.ordenAscendente ? -1 : 1;
      if (propA > propB) return this.ordenAscendente ? 1 : -1;
      return 0;
    });
  }

  get articulosPaginados() {
    const inicio = (this.paginaActual - 1) * this.registrosPorPagina;
    return this.articulosFiltradosOrdenados.slice(inicio, inicio + this.registrosPorPagina);
  }

  get totalPaginas() {
    return Math.ceil(this.articulosFiltradosOrdenados.length / this.registrosPorPagina);
  }

  cambiarPagina(offset: number) {
    const nuevaPagina = this.paginaActual + offset;
    if (nuevaPagina >= 1 && nuevaPagina <= this.totalPaginas) {
      this.paginaActual = nuevaPagina;
    }
  }

  ordenarPor(columna: string) {
    if (this.ordenColumna === columna) {
      this.ordenAscendente = !this.ordenAscendente;
    } else {
      this.ordenColumna = columna;
      this.ordenAscendente = true;
    }
  }

  // ✅ FUNCIÓN CORREGIDA - Abre/cierra modal sin resetear automáticamente
  toggleFormulario() {
    this.mostrarFormulario = !this.mostrarFormulario;
    
    // Solo resetear si se está cerrando el modal
    if (!this.mostrarFormulario) {
      this.resetearFormulario();
    }
  }

  async submit() {
    if (this.form.invalid) return;

    const formData = {
      ...this.form.value,
      imagen: this.imagenBase64
    };

    try {
      if (this.editandoId === null) {
        await this.articuloService.create(formData);
        Swal.fire('¡Creado!', 'Artículo creado exitosamente', 'success');
      } else {
        await this.articuloService.update(this.editandoId, formData);
        Swal.fire('¡Actualizado!', 'Artículo actualizado', 'success');
      }

      this.cancelarEdicion();
      await this.cargarArticulos();
    } catch (err) {
      Swal.fire('Error', 'No se pudo guardar el artículo', 'error');
    }
  }

  editar(articulo: ArticuloDto) {
    this.mostrarFormulario = true;
    this.editandoId = articulo.idArticulo;
    this.form.patchValue(articulo);
    this.imagenBase64 = articulo.imagen || null;
    this.imagenPreview = articulo.imagen ? `data:image/png;base64,${articulo.imagen}` : null;
  }

  // ✅ FUNCIÓN CORREGIDA - Cierra modal y resetea formulario
  cancelarEdicion() {
    this.resetearFormulario();
    this.mostrarFormulario = false; // Cierra el modal
  }

  // ✅ NUEVA FUNCIÓN - Solo resetea formulario sin cerrar modal
  private resetearFormulario() {
    this.form.reset();
    this.editandoId = null;
    this.imagenBase64 = null;
    this.imagenPreview = null;
  }

  async eliminar(id: number) {
    const result = await Swal.fire({
      title: '¿Estás seguro?',
      text: 'No podrás deshacer esta acción',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    });

    if (!result.isConfirmed) return;

    try {
      await this.articuloService.delete(id);
      this.articulos = this.articulos.filter(a => a.idArticulo !== id);
      Swal.fire('Eliminado', 'Artículo eliminado correctamente', 'success');
    } catch {
      Swal.fire('Error', 'No se pudo eliminar el artículo', 'error');
    }
  }
}