import { Component, signal, ChangeDetectorRef } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './core/services/auth.service';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import Swal from 'sweetalert2';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('frontend-dbpedidos');

  public currentYear: number = new Date().getFullYear();
  public isAuthPage = false;

  constructor(
    private router: Router, 
    public authService: AuthService,
    private cdr: ChangeDetectorRef
  ) {
    // Escuchar cambios de ruta para detectar páginas de auth
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      this.isAuthPage = event.url.includes('/login') || event.url.includes('/register');
      console.log('Current route:', event.url, 'isAuthPage:', this.isAuthPage);
      this.cdr.detectChanges();
    });
  }

  get userName(): string | null {
    const name = this.authService.getUserName();
    console.log('Getting username:', name);
    return name;
  }

  // Método para refrescar manualmente el usuario
  refreshUser() {
    this.cdr.detectChanges();
  }

  logout() {
  Swal.fire({
    title: '¿Estás seguro?',
    text: '¿Deseas cerrar sesión?',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Sí, cerrar sesión',
    cancelButtonText: 'Cancelar'
  }).then((result) => {
    if (result.isConfirmed) {
      this.authService.logout();
      this.router.navigate(['/login']);
      Swal.fire('¡Hasta luego!', 'Tu sesión ha sido cerrada.', 'success');
    }
  });
}
}
