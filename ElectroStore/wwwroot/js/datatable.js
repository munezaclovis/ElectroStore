$(document).ready(function () {

    $('#datatable').DataTable();

    function Display(object) {
        if (object.brand) {
            return Brand(object);
        } else if (object.product) {
            return Product(object);
        } else if (object.user) {
            return User(object);
        } else if (object.category) {
            return Category(object);
        }
    }

    function Brand(object) {
        var html = '<td>' + object.brand.id + '</td>';
        html += '<td>' + object.brand.name + '</td>';
        html += '<td for="status">';
        if (object.brand.deleted === true) {
            html += '<span class="badge badge-soft-danger">Deleted</span>';
        } else {
            html += '<span class="badge badge-soft-success">Active</span>';
        }
        html += '</td>';
        html += '<td for="buttons">';
        html += '<button class="btn btn-info mr-2" for="Edit" data-url="' + window.location.pathname + '/Edit' + '"><i class="dripicons-pencil mr-1"></i> Edit </button>';
        if (object.brand.deleted === true) {
            html += '<button class="btn btn-soft-warning" for="Restore"><i class="dripicons-clockwise mr-1"></i>Restore</button>';
        } else {
            html += '<button class="btn btn-soft-danger" for="Delete"><i class="dripicons-trash mr-1"></i>Delete</button>';
        }
        html += '</td>';
        return html;
    }

    function Category(object) {
        var html = '<td>' + object.category.id + '</td>';
        html += '<td>' + object.category.name + '</td>';
        html += '<td for="status">';
        if (object.category.deleted === true) {
            html += '<span class="badge badge-soft-danger">Deleted</span>';
        } else {
            html += '<span class="badge badge-soft-success">Active</span>';
        }
        html += '</td>';
        html += '<td for="buttons">';
        html += '<button class="btn btn-info mr-2" for="Edit" data-url="' + window.location.pathname + 'Edit' + '"><i class="dripicons-pencil mr-1"></i> Edit </button>';
        if (object.category.deleted === true) {
            html += '<button class="btn btn-soft-warning" for="Restore"><i class="dripicons-clockwise mr-1"></i>Restore</button>';
        } else {
            html += '<button class="btn btn-soft-danger" for="Delete"><i class="dripicons-trash mr-1"></i>Delete</button>';
        }
        html += '</td>';
        return html;
    }


    function Product(object) {
        var html = '<td>' + object.product.id + '</td>';
        html += '<td>' + object.product.name + '</td>';
        html += '<td>' + object.product.brandid + '</td>';
        html += '<td>' + object.product.categoryid + '</td>';
        html += '<td>' + object.product.dateadded + '</td>';
        html += '<td>' + object.product.dateupdated + '</td>';
        html += '<td>' + object.product.price + '</td>';
        html += '<td for="status">';
        if (object.product.deleted === true) {
            html += '<span class="badge badge-soft-danger">Deleted</span>';
        } else {
            html += '<span class="badge badge-soft-success">Active</span>';
        }
        html += '</td>';
        html += '<td for="buttons">';
        html += '<button class="btn btn-info mr-2" for="Edit" data-url="' + window.location.pathname + 'Edit' + '"><i class="dripicons-pencil mr-1"></i> Edit </button>';
        if (object.product.deleted === true) {
            html += '<button class="btn btn-soft-warning" for="Restore"><i class="dripicons-clockwise mr-1"></i>Restore</button>';
        } else {
            html += '<button class="btn btn-soft-danger" for="Delete"><i class="dripicons-trash mr-1"></i>Delete</button>';
        }
        html += '</td>';
        return html;
    }


    function User(object) {

    }

    function getForm(object, placeholder) {
        var url = $(object).data('url');
        var id = "";
        if ($(object).attr("for") === "Edit") {
            id = $(object).parent().parent().data("id");
            url = url.concat('/' + id);
        }

        $.get(url).done(function (data) {
            placeholder.append(data);
            placeholder.find('.modal').modal('show');
        });
    }


    var modalPlaceHolder = $('body');

    $('#datatable').on('click', 'tbody tr td button[data-toggle="modal"]', function (event) {
        getForm(this, modalPlaceHolder);
    });

    $('button[for="Add"]').on('click', function (event) {
        getForm(this, modalPlaceHolder);
    });

    modalPlaceHolder.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (result) {
            try {
                console.log(typeof result);

                if (typeof result === 'object') {
                    if (result.status === "Error") {
                        throw 'Error';
                    }
                } else if (typeof result === 'string') {
                    throw 'Undefinded';
                }

                var html = '<tr data-id="' + result.id + '">';
                html += Display(result);
                html += '</tr>';

                $('#datatable').find('tbody').append(html);
                modalPlaceHolder.find('.modal').modal('hide');
            } catch (e) {
                if (e === "Error") {
                    form.find(".validation-summary-errors").find('ul').append('<li>' + result.Message + '</li>');
                    return;
                }
                modalPlaceHolder.find('.modal').html($(result).find('.modal-dialog'));
                modalPlaceHolder.find('.modal').modal('show');
                //console.log(result);
            }
        });
    });

    $('#datatable').on('click', '[data-edit="modal"]', function (event) {
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();

        $.post(actionUrl, sendData).done(function (result) {
            console.log(result);
            return;
            try {
                if (typeof result === 'object') {
                    if (result.status === "Error") {
                        throw 'Error';
                    }
                } else if (typeof resuly === 'string') {
                    throw 'Undefinded';
                }

                var html = Display(result);
                $('tr[data-id="' + result.id + '"]').html(html);

                modalPlaceHolder.find('.modal').modal('hide');
            } catch (e) {
                if (e === "Error") {
                    form.find(".validation-summary-errors").find('ul').append('<li>' + result.Message + '</li>');
                    return;
                }
                modalPlaceHolder.find('.modal').html($(result).find('.modal-dialog'));
                modalPlaceHolder.find('.modal').modal('show');
            }
        });
    });

    $(this).on('hidden.bs.modal', '.modal', function () {
        this.remove();
    });


    $('#datatable').on('click', 'button', function () {
        var row = $(this).closest('td').closest('tr');
        var id = row.data('id');
        var url = (window.location.pathname).concat('/' + $(this).attr("for"));
        switch ($(this).attr('for')) {
            case "Delete":
                var newAction = "Restore";
                $.ajax({
                    url: url,
                    method: 'POST',
                    data: { id: id },
                    success: function (response) {
                        console.log(response);
                        row.find('td[for="status"]')
                            .find('span')
                            .removeClass('badge-soft-success')
                            .addClass('badge-soft-danger')
                            .text('Deleted');
                        row.find('td[for="buttons"]').find('button[for="Delete"]').remove();
                        row.find('td[for="buttons"]').append('<button class="btn btn-soft-warning" for="' + newAction + '"><i class="dripicons-clockwise mr-1"></i>' + newAction + '</button>');
                    },
                    error: function (error) {
                        console.log(error);
                    },
                    cache: false
                });
                break;
            case "Restore":
                var newAction = "Delete";
                $.ajax({
                    url: url,
                    method: 'POST',
                    data: { id: id },
                    success: function (response) {
                        console.log(response);
                        row.find('td[for="status"]')
                            .find('span')
                            .removeClass('badge-soft-danger')
                            .addClass('badge-soft-success')
                            .text('Active');
                        row.find('td[for="buttons"]').find('button[for="Restore"]').remove();
                        row.find('td[for="buttons"]').append('<button class="btn btn-soft-danger" for="' + newAction + '"><i class="dripicons-trash mr-1"></i>' + newAction + '</button>');
                    },
                    error: function (error) {
                        console.log(error);
                    },
                    cache: false
                });
                break;
        }
    });
});

