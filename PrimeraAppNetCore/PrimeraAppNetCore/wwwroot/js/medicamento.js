window.onload = function () {
    mostrar();
}

let obj = {
    url: 'Medicamentos/listarMedicamentos',
    cabeceras: ['Id', 'Nombre', 'Descripcion'],
    propiedades: ['id', 'nombre', 'descripcion']
};

function filtrarDinamico() {
    let descMedicamento = getValue('descMedicamentoInput');

    if (descMedicamento == "")
        mostrar();
    else
        filtrar();
}

async function mostrar() {
    obj.url = 'Medicamentos/listarMedicamentos';
    mostrarTabla(obj);
}

async function filtrar() {
    let descMedicamento = getValue('descMedicamentoInput');
    obj.url = 'Medicamentos/filtrarMedicamentos?descripcionFiltrar=' + descMedicamento;
    mostrarTabla(obj);
}

async function limpiar() {
    setValue('descMedicamentoInput', '');
    filtrar();
    mostrarTabla(objSucursal);
}