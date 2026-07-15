let a = ['z', 'x', 'm', 'ñ', 'o', 'n', 'w', 'g', 'h', 'i', 'j'];

function busquedaLineal(array, elemento) {
  for (let i = 0; i < array.length; i++) {
    if (array[i] === elemento) {
      return i; // Retorna el índice del elemento encontrado
    }
  }
  return -1; // Retorna -1 si el elemento no se encuentra
}