var ViewModel = function () {
  var self = this;
  self.dentists = ko.observableArray();
  self.error = ko.observable();

  var dentistUri = '/api/Dentists/';

  function ajaxHelper(uri, method, data) {
    self.error(''); // Clear error message
    return $.ajax({
      type: method,
      url: uri,
      dataType: 'json',
      contentType: 'application/json',
      data: data ? JSON.stringify(data) : null
    }).fail(function (jqXHR, textStatus, errorThrown) {
      self.error(errorThrown);
    });
  }

  function getAllDentists() {
    ajaxHelper(dentistUri, 'GET').done(function (data) {
      self.dentists(data);
    });
  }

  // Fetch the initial data.
  getAllDentists();
};

ko.applyBindings(new ViewModel());

function formatDate(odate) {
  var date = new Date(odate);
  var year = date.getFullYear();
  var month = date.getMonth() + 1;
  var options = { month: 'long' };
  var fullMonth = new Intl.DateTimeFormat('en-US', options).format(date);
  var day = date.getDate();
  return fullMonth + ' ' + day + ', ' + year;
}