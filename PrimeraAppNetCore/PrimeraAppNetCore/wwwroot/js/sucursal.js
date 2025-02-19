window.onload = function () {
    mostrar();
}

let objSucursal = {
    url: '',
    cabeceras: ['Id', 'Nombre', 'Dirección'],
    propiedades: ['idSucursal', 'nombre', 'direccion']
}

async function mostrar() {
    objSucursal.url = 'Sucursal/ListarSucursales';  
    mostrarTabla(objSucursal);  
}
function buscar() {
    let formData = new FormData(document.getElementById('formBuscar')); 

    fetchPost('Sucursal/FiltrarSucursales', 'json', formData, (res) => {
        let tabla = document.createElement('table');  
        document.getElementById('divtabla').innerHTML = ''; 
        document.getElementById('divtabla').appendChild(tabla); 
        tabla.classList.add('table', 'table-striped', 'table-bordered');  
        tabla.innerHTML = generarTabla(objSucursal, res);  
    });
}
function limpiar() {
    document.getElementById('formBuscar').reset(); 
    mostrar();  
}
