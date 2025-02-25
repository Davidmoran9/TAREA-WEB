﻿function get(idControl) {
    return document.getElementById(idControl).value;
}

function set(idControl, valor) {
    document.getElementById(idControl).value = valor;
}

function setN(namecontrol, valor) {
    document.getElementsByName(namecontrol)[0].value = valor;
}

function LimpiarDatos(idFormulario) {
    let elementosName = document.querySelectorAll('#' + idFormulario + " [name]");
    for (let elemento of elementosName) {
        setN(elemento.name, "");
    }
}

async function fetchGet(url, tipoRespuesta, callback) {
    try {
        let urlCompleta = `${window.location.protocol}//${window.location.host}/${url}`;
        let res = await fetch(urlCompleta);

        if (tipoRespuesta === "json") {
            res = await res.json();
        } else if (tipoRespuesta === "text") {
            res = await res.text();
        }

        callback(res);
    } catch (e) {
        alert("Ocurrió un problema: " + e.message);
    }
}

async function fetchPost(url, tipoRespuesta, frm, callback) {
    try {
        let urlCompleta = `${window.location.protocol}//${window.location.host}/${url}`;

        let res = await fetch(urlCompleta, {
            method: "POST",
            body: frm
        });

        if (tipoRespuesta === "json") {
            res = await res.json();
        } else if (tipoRespuesta === "text") {
            res = await res.text();
        }

        callback(res);
    } catch (e) {
        alert("Ocurrió un problema en POST");
    }
}

let objConfiguracionGlobal;

function pintar(objConfiguracion) {
    objConfiguracionGlobal = objConfiguracion;

    objConfiguracionGlobal.divContenedorTabla = objConfiguracionGlobal.divContenedorTabla || "divContenedorTabla";
    objConfiguracionGlobal.editar = objConfiguracionGlobal.editar || false;
    objConfiguracionGlobal.eliminar = objConfiguracionGlobal.eliminar || false;

    fetchGet(objConfiguracion.url, "json", function (res) {
        let contenido = `<div id='divContenedorTabla'>${generarTabla(res)}</div>`;
        document.getElementById("divTable").innerHTML = contenido;
    });
}

function generarTabla(res) {
    let contenido = "<table class='table'>";
    contenido += "<thead><tr>";

    for (let cabecera of objConfiguracionGlobal.cabeceras) {
        contenido += `<th>${cabecera}</th>`;
    }

    if (objConfiguracionGlobal.editar || objConfiguracionGlobal.eliminar) {
        contenido += "<th>Operaciones</th>";
    }

    contenido += "</tr></thead><tbody>";

    for (let obj of res) {
        contenido += "<tr>";

        for (let propiedad of objConfiguracionGlobal.propiedades) {
            contenido += `<td>${obj[propiedad]}</td>`;
        }

        if (objConfiguracionGlobal.editar || objConfiguracionGlobal.eliminar) {
            contenido += "<td>";

            if (objConfiguracionGlobal.editar) {
                contenido += `
                <i onclick="editar(${obj[objConfiguracionGlobal.propiedadesID]})" class="btn btn-primary">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                        <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                        <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"/>
                    </svg>
                </i>`;
            }

            if (objConfiguracionGlobal.eliminar) {
                contenido += `
                <i onclick="eliminar(${obj[objConfiguracionGlobal.propiedadesID]})" class="btn btn-danger">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">
                        <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5m-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5M4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06m6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528M8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5"/>
                    </svg>
                </i>`;
            }

            contenido += "</td>";
        }

        contenido += "</tr>";
    }

    contenido += "</tbody></table>";
    return contenido;
}