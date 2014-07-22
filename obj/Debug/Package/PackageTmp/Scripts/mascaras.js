/* Convierte a mayúsculas*/
function aMayusculas(elemento) {
    var val = elemento.value;
    elemento.value = val.toUpperCase().trim();
}

/* Convierte  a minúsculas */
function aMinusculas(elemento) {
    var val = elemento.value;
    elemento.value = val.toLowerCase().trim();
}