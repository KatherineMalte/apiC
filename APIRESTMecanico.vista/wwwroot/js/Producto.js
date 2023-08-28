const _modeloProducto = {

    idProducto: 0,
    nombreProducto: "",
    precioProducto: "",
    stok: 0,
    IdCategoria: 0
}

function MostrarProductos() {

    fetch("/Home/listarProducto")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#tablaProductos tbody").html("");


                responseJson.forEach((empleado) => {
                    $("#tablaProductos tbody").append(
                        $("<tr>").append(
                            $("<td>").text(empleado.nombreProducto),
                            $("<td>").text(empleado.precioProducto),
                            $("<td>").text(empleado.stok),
                            $("<td>").text(empleado.categoria.categoria),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-producto").text("Editar").data("dataProducto", empleado),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-producto").text("Eliminar").data("dataProducto", empleado),
                            )
                        )
                    )
                })

            }


        })

}
document.addEventListener("DOMContentLoaded", function () {

    MostrarProductos();

    fetch("/Home/GetCategoria")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {

            if (responseJson.length > 0) {
                responseJson.forEach((item) => {

                    $("#cboDepartamento").append(
                        $("<option>").val(item.idCategoria).text(item.categoria)
                    )

                })
            }

        })

}, false)


function MostrarModal() {

    $("#txtNombreProducto").val(_modeloProducto.nombreProducto);
    $("#cboDepartamento").val(_modeloProducto.idDepartamento == 0 ? $("#cboDepartamento option:first").val() : _modeloProducto.idCategoria)
    $("#txtPrecioProducto").val(_modeloProducto.precioProducto);
    $("#txtStok").val(_modeloProducto.stok)


    $("#modalProducto").modal("show");

}

$(document).on("click", ".boton-nuevo-producto", function () {

    _modeloProducto.idProducto = 0;
    _modeloProducto.nombreProducto = "";
    _modeloProducto.idCategoria = 0;
    _modeloProducto.stok = 0;
    _modeloProducto.precioProducto = "";

    MostrarModal();

})

$(document).on("click", ".boton-editar-producto", function () {

    const _prod = $(this).data("dataProducto");

    _modeloProducto.idProducto = _prod.idProducto;
    _modeloProducto.nombreProducto = _prod.nombreProducto;
    _modeloProducto.idCategoria = _prod.categoria.categoria;
    _modeloProducto.stok = _prod.stok;
    _modeloProducto.precioProducto = _prod.precioProducto;
    MostrarModal();

})

$(document).on("click", ".boton-guardar-cambios-producto", function () {

    const modelo = {
        idProducto: _modeloProducto.idProducto,
        nombreProducto: $("#txtNombreProducto").val(),
        Categoria: {
            IdCategoria: $("#cboDepartamento").val()
        }, stok: $("#txtStok").val(),
        precioProducto: $("#txtPrecioProducto").val()

    }


    if (_modeloProducto.idProducto == 0) {

        fetch("/Home/guardarProducto", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalProducto").modal("hide");
                    Swal.fire("Listo!", "Empleado fue creado", "success");
                    MostrarProductos();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo crear", "error");
            })

    } else {

        fetch("/Home/editarProducto", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalProducto").modal("hide");
                    Swal.fire("Listo!", "Empleado fue actualizado", "success");
                    MostrarProductos();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo actualizar", "error");
            })

    }


})


$(document).on("click", ".boton-eliminar-producto", function () {

    const _empleado = $(this).data("dataProducto");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar empleado "${_empleado.nombreCompleto}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Home/eliminarEmpleado?idEmpleado=${_empleado.idEmpleado}`, {
                method: "DELETE"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "Empleado fue elminado", "success");
                        MostrarProductos();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo eliminar", "error");
                })

        }



    })

})