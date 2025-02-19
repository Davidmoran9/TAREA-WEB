window.onload = function () {
    mostrar();
}

let obj = {
    url: '',
    cabeceras: ['Id', 'Nombre', 'Dirección', 'Persona de Contacto'],
    propiedades: ['idLaboratorio', 'nombre', 'direccion', 'personaContacto']
}

async function mostrar() {
    obj.url = 'Laboratorio/ListarLaboratorios';
    mostrarTabla(obj);
}

function buscar() {
    let formData = new FormData(document.getElementById('formBuscar'));
    fetchPost('Laboratorio/Filtrarlaboratorio', 'json', formData, (res) => {
        let tabla = document.createElement('table');
        document.getElementById('divtabla').innerHTML = '';
        document.getElementById('divtabla').appendChild(tabla);
        tabla.classList.add('table', 'table-striped', 'table-bordered');
        tabla.innerHTML = generarTabla(obj, res);
    });
}

function limpiar() {
    document.getElementById('formBuscar').reset();
    mostrar();
}