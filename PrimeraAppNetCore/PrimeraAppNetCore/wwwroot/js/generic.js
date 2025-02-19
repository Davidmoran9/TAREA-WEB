async function fetchGet(url, resTipo, callback) {
    try {
        let raiz = getValue('hiddenInput');
        let urlCompleta = window.location.protocol + '//' + window.location.host + '/' + raiz + url;

        let res = await fetch(urlCompleta);
        if (resTipo === 'json') res = await res.json();
        if (resTipo === 'text') res = await res.text();

        callback(res);
    } catch (e) {
        alert(e);
    }
}

async function fetchPost(url, resTipo, form, callback) {
    try {
        let raiz = getValue('hiddenInput');
        let urlCompleta = window.location.protocol + '//' + window.location.host + '/' + raiz + url;

        let res = await fetch(urlCompleta, {
            method: 'POST',
            body: form
        });
        if (resTipo === 'json') res = await res.json();
        if (resTipo === 'text') res = await res.text();

        callback(res);
    } catch (e) {
        alert(e);
    }
}

function getValue(id) {
    return document.getElementById(id).value;
}

function setValue(id, value) {
    document.getElementById(id).value = value;
}

function setName(name) {
    return document.getElementsByName(name)[0].value;
}

function mostrarTabla(config) { //url, cabeceras[], propiedades[]
    //if (config == undefined)

    fetchGet(config.url, 'json', (res) => {
        let tabla = document.createElement('table');
        document.getElementById('divtabla').innerHTML = '';
        document.getElementById('divtabla').appendChild(tabla);
        tabla.classList.add('table', 'table-striped', 'table-bordered');
        tabla.innerHTML = generarTabla(config, res);
    });
}

function generarTabla(config, res) {
    let contenido = '';

    contenido += '<thead><tr>';
    for (let cabecera of config.cabeceras) contenido += `<th>${cabecera}</th>`;
    contenido += '</tr></thead>';

    contenido += '<tbody>';
    for (let obj of res) {
        contenido += '<tr>';
        for (propiedad of config.propiedades) contenido += `<td>${obj[propiedad]}</td>`;
        contenido += '</tr>';
    }
    contenido += '</tbody>';

    return contenido;
}   