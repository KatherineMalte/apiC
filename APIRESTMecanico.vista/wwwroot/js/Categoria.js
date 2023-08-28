const _modeloCategoria = {
    idCategoria: 0,
    categoria: "",
    codigoCategoria: "",
    nombre: "",
    observacion: ""
}

function MostrarCategoria() {
    fetch("/Home/GetCategoria")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                $("#tablaCategoria tbody").html("");
                responseJson.forEach((categoria) => {
                    $("#tablaCategoria tbody").append(
                        $("<tr>").append(
                            $("<td>").text(categoria.categoria),
                            $("<td>").text(categoria.codigoCategoria),
                            $("<td>").text(categoria.nombre),
                            $("<td>").text(categoria.observacion),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-categoria").text("Editar").data("dataCategoria", categoria),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-categoria").text("Eliminar").data("dataCategoria", categoria),
                            )
                        )
                    )

                })
            }
        })
}

document.addEventListener("DOMContentLoaded", function () {
    MostrarCategoria();
}, false)

function MostrarModal() {
    $("#txtCategoria").val(_modeloCategoria.categoria);
    $("#txtNombre").val(_modeloCategoria.nombre);
    $("#txtObservacion").val(_modeloCategoria.observacion);
    $("#modalCategoria").modal("show");
}
$(document).on("click", ".boton-nuevo-categoria", function () {
    _modeloCategoria.idCategoria = 0;
    _modeloCategoria.categoria = "",
    _modeloCategoria.codigoCategoria = "";
    _modeloCategoria.nombre = "";
    _modeloCategoria.observacion = "";
    MostrarModal();
})

$(document).on("click", ".boton-editar-categoria", function () {
    const _modeloCategoriaProductos = $(this).data("dataCategoria");

    _modeloCategoria.idCategoria = _modeloCategoriaProductos.idCategoria;
    _modeloCategoria.categoria = _modeloCategoriaProductos.categoria;
    _modeloCategoria.codigoCategoria = _modeloCategoriaProductos.codigoCategoria;
    _modeloCategoria.nombre = _modeloCategoriaProductos.nombre;
    _modeloCategoria.observacion = _modeloCategoriaProductos.observacion;
    MostrarModal();

})

$(document).on("click", ".boton-guardar-cambios-categoria", function () {

    const modelo = {
        idCategoria: _modeloCategoria.idCategoria,
        categoria: $("#txtCategoria").val(),
        nombre: $("#txtNombre").val(),
        observacion: $("#txtObservacion").val()
    }
    if (_modeloCategoria.idCategoria == 0) {

        fetch("/Home/insertarCategoria", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalCategoria").modal("hide");
                    Swal.fire("Listo!", "Categoria fue creado", "success");
                    MostrarCategoria();
                }
                else
                    Swal.fire("Lo sentimos", "No puedo crear", "error");
            })

    } else {

        fetch("/Home/actualizarCategoria", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalCategoria").modal("hide");
                    Swal.fire("Listo!", "Categoria fue actualizado", "success");
                    MostrarCategoria();
                }
                else
                    Swal.fire("Lo sentimos", "No puedo actualizar", "error");
            })

    }


})

$(document).on("click", ".boton-eliminar-categoria", function () {

    const _modeloCategoria = $(this).data("dataCategoria");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar categoria "${_modeloCategoria.nombre}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Home/eliminarCategoria?idCategoria=${_modeloCategoria.idCategoria}`, {
                method: "DELETE"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "Categoria fue elminado", "success");
                        MostrarCategoria();
                    }
                    else
                        Swal.fire("Lo sentimos", "No puedo eliminar", "error");
                })

        }



    })

})