const _modeloCliente = {
    idCliente: 0,
    nombreCliente: "",
    apellidoCliente: "",
    direccionCliente: "",
    paisCliente: "",
    fechaNacimiento: "",
    telefonoCliente: 0,
    emailCliente: "",
    numeroID: 0
}

function MostrarCliente() {
    fetch("/Home/listarCliente")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })

        .then(responseJson => {
            if (responseJson.length > 0) {
                $("#tablaCliente tbody").html("");
                responseJson.forEach((categoria) => {
                    $("#tablaCliente tbody").append(
                        $("<tr>").append(
                            $("<td>").text(categoria.numeroID),
                            $("<td>").text(categoria.nombreCliente),
                            $("<td>").text(categoria.apellidoCliente),
                            $("<td>").text(categoria.direccionCliente),
                            $("<td>").text(categoria.paisCliente),
                            $("<td>").text(categoria.fechaNacimiento),
                            $("<td>").text(categoria.telefonoCliente),
                            $("<td>").text(categoria.emailCliente),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-cliente").text("Editar").data("dataCliente", categoria),
                                // $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-cliente").text("Eliminar").data("dataCliente", categoria),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-obtener-cliente").text("obtener").data("dataCliente", categoria),)
                        )
                    )

                })
            }
        })
}
document.addEventListener("DOMContentLoaded", function () {
    MostrarCliente();
    $("#txtFechaN").datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight: true
    })

}, false)

function MostrarModal() {
    $("#txtDocumento").val(_modeloCliente.numeroID);
    $("#txtNombre").val(_modeloCliente.nombreCliente);
    $("#txtApellido").val(_modeloCliente.apellidoCliente);
    $("#txtDir").val(_modeloCliente.direccionCliente);
    $("#txtPais").val(_modeloCliente.paisCliente);
    $("#txtFechaN").val(_modeloCliente.fechaNacimiento);
    $("#txtTelefono").val(_modeloCliente.telefonoCliente);
    $("#txtEmail").val(_modeloCliente.emailCliente);
    $("#modalCliente").modal("show");
}
$(document).on("click", ".boton-nuevo-cliente", function () {
    _modeloCliente.idCliente = 0;
    _modeloCliente.numeroID = 0;
    _modeloCliente.nombreCliente = "";
    _modeloCliente.apellidoCliente = "";
    _modeloCliente.direccionCliente = "";
    _modeloCliente.paisCliente = "";
    _modeloCliente.fechaNacimiento = "";
    _modeloCliente.telefonoCliente = 0;
    _modeloCliente.emailCliente = "";
    MostrarModal();
})

$(document).on("click", ".boton-editar-cliente", function () {
    const _modeloClienteProductos = $(this).data("dataCliente");

    _modeloCliente.idCliente = _modeloClienteProductos.idCliente;
    _modeloCliente.numeroID = _modeloClienteProductos.numeroID;
    _modeloCliente.nombreCliente = _modeloClienteProductos.nombreCliente;
    _modeloCliente.apellidoCliente = _modeloClienteProductos.apellidoCliente;
    _modeloCliente.direccionCliente = _modeloClienteProductos.direccionCliente;
    _modeloCliente.paisCliente = _modeloClienteProductos.paisCliente;
    _modeloCliente.fechaNacimiento = _modeloClienteProductos.fechaNacimiento;
    _modeloCliente.telefonoCliente = _modeloClienteProductos.telefonoCliente;
    _modeloCliente.emailCliente = _modeloClienteProductos.emailCliente;
    MostrarModal();

})

$(document).on("click", ".boton-guardar-cambios-cliente", function () {

    const modelo = {
        idCliente: _modeloCliente.idCliente,
        numeroID: $("#txtDocumento").val(),
        nombreCliente: $("#txtNombre").val(),
        apellidoCliente: $("#txtApellido").val(),
        direccionCliente: $("#txtDir").val(),
        paisCliente: $("#txtPais").val(),
        fechaNacimiento: $("#txtFechaN").val(),
        telefonoCliente: $("#txtTelefono").val(),
        emailCliente: $("#txtEmail").val(),

    }
    if (_modeloCliente.idCliente == 0) {

        fetch("/Home/guardarCliente", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalCliente").modal("hide");
                    Swal.fire("Listo!", "Categoria fue creado", "success");
                    MostrarCategoria();
                }
                else
                    Swal.fire("Lo sentimos", "No puedo crear", "error");
            })

    } else {

        fetch("/Home/actualizarCliente", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalCliente").modal("hide");
                    Swal.fire("Listo!", "Categoria fue actualizado", "success");
                    MostrarCategoria();
                }
                else
                    Swal.fire("Lo sentimos", "No puedo actualizar", "error");
            })

    }


})
$(document).on("click", ".boton-obtener-cliente", function () {
    const modelo = {
        numeroID: $("#txtDocumento").val(),
        nombreCliente: $("#txtNombre").val(),
        apellidoCliente: $("#txtApellido").val(),
        direccionCliente: $("#txtDir").val(),
        telefonoCliente: $("#txtTelefono").val(),
        emailCliente: $("#txtEmail").val(),
    }
    if (modelo.numeroID != 0) {
        fetch(`/Home/traerClientePorId?idCliente=${modelo.numeroID}`, {
            method: "GET"
        }).then(response => {
            return response.ok ? response.json() : Promise.reject(response)

        })
            .then(responseJson => {

                // if (responseJson.valor) {
                //     document.getElementById("txtNombre").value = json["nombreCliente"];

                document.getElementById("txtNombre").value = responseJson[0].nombreCliente;
                document.getElementById("txtEmail").value = responseJson[0].emailCliente;
                // document.getElementById("txtTelefono").value = responseJson[0].telefonoCliente;
                document.getElementById("txtDir").value = responseJson[0].direccionCliente;
                //document.getElementById("#txtDocumento").value = responseJson[0].numeroID;


            })

        MostrarModal();

    }
})

$(document).on("click", ".boton-eliminar-categoria", function () {

    const _modeloCliente = $(this).data("dataCliente");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar categoria "${_modeloCliente.nombre}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Home/eliminarCategoria?idCategoria=${_modeloCliente.idCategoria}`, {
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

