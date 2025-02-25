window.onload = function () {
    listarTiposMedicamento();
};

function filtrarTiposMedicamento() {
    let nombre = get("txtTipoMedicamento");
    if (nombre == "") {
        listarTiposMedicamento();
    } else {
        objTiposMedicamento.url = "TipoMedicamento/filtrarTiposMedicamento/?nombre=" + nombre;
        pintar(objTiposMedicamento);
    }
}

let objTiposMedicamento;

async function listarTiposMedicamento() {
    let objTiposMedicamento = {
        url: "TipoMedicamento/listarTiposMedicamento",
        cabeceras: ["ID Tipo Medicamento", "Nombre", "Descripción"],
        propiedades: ["iidTipoMedicamento", "nombre", "descripcion"],
        editar: true,
        eliminar: true,
        propiedadesID: "iidTipoMedicamento"
    };

    pintar(objTiposMedicamento);
}
function BuscarTipoMedicamento() {
    let forma = document.getElementById("frmBusqueda");

    let frm = new FormData(forma);

    fetchPost("TipoMedicamento/filtrarTiposMedicamento", "json", frm, function (res) {
        document.getElementById("divTable").innerHTML = generarTabla(res);
    });
}

function LimpiarTipoMedicamento() {
    LimpiarDatos("frmGuardarTipoMedicamento");
    listarTiposMedicamento();
}

function GuardarTipoMedicamento() {
    let forma = document.getElementById("frmGuardarTipoMedicamento");
    let frm = new FormData(forma);
    fetchPost("TipoMedicamento/GuardarTipoMedicamento", "text", frm, function (res) {
        listarTiposMedicamento();
        LimpiarDatos("frmGuardarTipoMedicamento");
    });
}
function editar(id) {
    alert(id);
    let url = `TipoMedicamento/recuperarTipoMedicamento/${id}`;
    fetchPost(url, "json", null, function (res) {
        console.log(res);
        setN("iidTipoMedicamento", data.iidTipoMedicamento)
        setN("nombre", data.nombre)
        setN("descripcion", data.descripcion)
    });

}
