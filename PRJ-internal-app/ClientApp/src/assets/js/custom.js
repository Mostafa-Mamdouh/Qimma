
////  Sidebar Control
//document.addEventListener("DOMContentLoaded", function(){
//    document.querySelectorAll('.sidebar .nav-link').forEach(function(element){
      
//      element.addEventListener('click', function (e) {
  
//        let nextEl = element.nextElementSibling;
//        let parentEl  = element.parentElement;	
  
//          if(nextEl) {
//              e.preventDefault();	
//              let mycollapse = new bootstrap.Collapse(nextEl);
              
//              if(nextEl.classList.contains('show')){
//                mycollapse.hide();
//              } else {
//                  mycollapse.show();
//                  // find other submenus with class=show
//                  var opened_submenu = parentEl.parentElement.querySelector('.submenu.show');
//                  // if it exists, then close all of them
//                  if(opened_submenu){
//                    new bootstrap.Collapse(opened_submenu);
//                  }
//              }
//          }
//      }); // addEventListener
//    }) // forEach
//  });
////   End of SideBar Control


//// Main Fav Select Control
//function favlist() {
//    document.getElementById("favDropDownList").classList.toggle("show");
//  }

//function filterFavList() {
//    var input, filter, ul, li, a, i;
//    input = document.getElementById("myInput");
//    filter = input.value.toUpperCase();
//    div = document.getElementById("favDropDownList");
//    a = div.getElementsByTagName("a");
//    for (i = 0; i < a.length; i++) {
//      txtValue = a[i].textContent || a[i].innerText;
//      if (txtValue.toUpperCase().indexOf(filter) > -1) {
//        a[i].style.display = "";
//      } else {
//        a[i].style.display = "none";
//      }
//    }
//  }

//// end of Main Fav Select Control






//// DataTable Identify
//$(document).ready(function() {
//  // Setup - add a text input to each footer cell
//  $('#datatablemain tfoot th').each( function () {
//      let title = $('#datatablemain thead th').eq( $(this).index() ).text();
//      $(this).html( '<input type="text" class="searchFieldDataTable" placeholder="'+title+'" />' );
//  } );

//  // DataTable
//  let table = $('#datatablemain').DataTable({
//    dom: 'Bfrtip',
    
//    buttons: [
//      { extend: 'copy', className: 'copyButton btn btn-gold btn-sm mx-1' },
//      { extend: 'excel', className: 'excelButton btn btn-gold btn-sm mx-1' },
//      { extend: 'csv', className: 'csvButton btn btn-gold btn-sm mx-1' },
//      // { extend: 'pdf', className: 'pdfButton btn btn-gold btn-sm mx-1' },
//      { extend: 'print', className: 'printButton btn btn-gold btn-sm mx-1' },
//      // 'csv', 'excel', 'pdf', 'print'
//  ],
//    select:true,
//    paging: false,
//    scrollY: 200,
//    columnDefs: [
//      { width: 100, targets: 5 },
//      { width: 100, targets: 3 },
//      {targets: 9 ,searchable : false,},
//      {target: 2, visible: false,searchable: false},
//    ],
//    aoColumnDefs: [
//      { Searchable: false, Targets: [ 9 ] }
//    ]
//  });

//  // Apply the search
//  table.columns().every( function () {
//      let that = this;

//      $( 'input', this.footer() ).on( 'keyup change', function () {
//          that
//              .search( this.value )
//              .draw();
//      } );
//  } );
//});

////Remove the BtnGroup class from datatable export buttons 

//$(document).ready(function(){
//  $('.dt-buttons').removeClass('btn-group');
//});






////form Select tooltip
//function getAttr(selectOption, id){
//  // this is print the selected Options Array
//  console.log(selectOption);
//  // this is print the ID of attribute
//  console.log('print the ID of Select: #'+ id);
//  let idd = '#'+id;
//  //console.log(idd);
//  //siblingsnextAll

//  let parentClass = $(idd).parent().prop('className');
//  // Print Parent Class Name
//  console.log('Parent Class : '+ parentClass);

//  if(selectOption.length > 0){
//  $('.' + parentClass).siblings('div').children('.selectParentCount').css('display','block');
//  }else{
//    $('.' + parentClass).siblings('div').children('.selectParentCount').css('display','none');
//  }

//};



//// ToolTip for filterSearch Pop Up


//$(document).on('click','.selectParentCount',function(){

    
//    let parentClassw =  $(this).parent().prop('className');
//    //console.log(parentClassw);
//    $('#exampleModal .modal-dialog .modal-content .modal-header .modal-title').empty().append(`${parentClassw} Details`);

//    $('#exampleModal .modal-dialog .modal-content .modal-body').empty().append(`
//          <table id="table_id" class="table table-bordered table-striped table-sm">
//          <thead>
//                                <tr>
//                                    <th>Name</th>
//                                    <th>Position</th>
//                                    <th>Office</th>
//                                    <th>Age</th>
//                                    <th>Start date</th>
//                                    <th>Salary</th>
//                                </tr>
//                            </thead>
                         
//                            <tfoot>
//                                <tr>
//                                    <th>Name</th>
//                                    <th>Position</th>
//                                    <th>Office</th>
//                                    <th>Age</th>
//                                    <th>Start date</th>
//                                    <th>Salary</th>
//                                </tr>
//                            </tfoot>
                         
//                            <tbody>
//                                <tr>
//                                    <td>Tiger Nixon</td>
//                                    <td>System Architect</td>
//                                    <td>Edinburgh</td>
//                                    <td>61</td>
//                                    <td>2011/04/25</td>
//                                    <td>$320,800</td>
//                                </tr>
//                                <tr>
//                                    <td>Garrett Winters</td>
//                                    <td>Accountant</td>
//                                    <td>Tokyo</td>
//                                    <td>63</td>
//                                    <td>2011/07/25</td>
//                                    <td>$170,750</td>
//                                </tr>
//                                <tr>
//                                    <td>Ashton Cox</td>
//                                    <td>Junior Technical Author</td>
//                                    <td>San Francisco</td>
//                                    <td>66</td>
//                                    <td>2009/01/12</td>
//                                    <td>$86,000</td>
//                                </tr>
//                                <tr>
//                                    <td>Cedric Kelly</td>
//                                    <td>Senior Javascript Developer</td>
//                                    <td>Edinburgh</td>
//                                    <td>22</td>
//                                    <td>2012/03/29</td>
//                                    <td>$433,060</td>
//                                </tr>
//                                <tr>
//                                    <td>Airi Satou</td>
//                                    <td>Accountant</td>
//                                    <td>Tokyo</td>
//                                    <td>33</td>
//                                    <td>2008/11/28</td>
//                                    <td>$162,700</td>
//                                </tr>
//                            </tbody>
//      </table>
//    `);

//    // Setup - add a text input to each footer cell
//  $('#table_id tfoot th').each( function () {
//    let title = $('#table_id thead th').eq( $(this).index() ).text();
//    $(this).html( '<input type="text" placeholder="Search '+title+'" />' );
//} );

//// DataTable

//let table = $('#table_id').DataTable({
//  select:true,
//});

//// Apply the search
//table.columns().every( function () {
//    let that = this;

//    $( 'input', this.footer() ).on( 'keyup change', function () {
//        that
//            .search( this.value )
//            .draw();
//    } );
//} );

//    $('#exampleModal').modal('show');
//});



//// Toggle Class when Click Button of Source
//$(document).on('click','.sourceIconItemFilter', function(e){
//  let check = $(this).hasClass('btn-lightww');
//  if(check){
//    $(this).removeClass('btn-lightww');
//    $(this).addClass('btn-secondary');
//    $(this).children('.souceLabel').css('color', 'var(--white)');
//  }else{
//    $(this).removeClass('btn-secondary');
//    $(this).addClass('btn-lightww');
//    $(this).children('.souceLabel').css('color', 'var(--dGray)');
//  }
//});

//// display the Div of Alert

//function showAlert() {
//  $('.searchFilterAlert').addClass('d-flex');
//  console.log('ID of Class' + this.id);

//  $('.searchFilterAlert').empty().append(`
//                                <div class="btn btn-lightww mx-2">
//                                Sealed without Shield
//                                </div>
//                                <div class="btn btn-lightww mx-2">
//                                Shield without Source
//                                </div>
//                                <div class="btn btn-lightww mx-2">
//                                Categoy 1 & 2
//                                </div>
//                                <button class="btn btn-close closesearchFilterAlert"></button>
//                                `);
//};



//// Button Close for Div Alert Details

//$(document).on('click', 'sourcealert > a', function(e){
//  e.preventDefault();
//  console.log('ID of Class' + this.id);
//  $('.sourcealert').show();

//  $('.searchFilterAlert').append(`
//                                <div class="btn btn-lightww mx-2">
//                                Sealed without Shield
//                                </div>
//                                <div class="btn btn-lightww mx-2">
//                                Shield without Source
//                                </div>
//                                <div class="btn btn-lightww mx-2">
//                                Categoy 1 & 2
//                                </div>

//                                `);

//});

//$(document).on('click', '.closesearchFilterAlert', function(e){
//  e.preventDefault();
//  $('.searchFilterAlert').removeClass('d-flex');
//  $('.searchFilterAlert').empty();
//});












//// ShowDetailsGrid



//function showDetailsGrid(){
//  $('.modal-body').empty().append(`
//      <img src="assets/imgs/addtransaction.jpg" class="w-100">                
//  `);

//  $('#exampleModal').modal('show');


//};



//// Filter Button
//$(document).on('click','.extra-filter-button', function(e){

//  e.preventDefault();
//  $('h5.modal-title').empty().append(`Filter Search..`)
//  $('.modal-body').empty().append(`
//      <div class="row">
//        <div class="col-md-12">
//            <h6>Classes: </h6>
//        </div>
//        <div class="col-md-4">
//          <select name="entityclass" id="entityclass" class="form-select m-1" placeholder="Entity">
//            <option value="-1">Entity</option>
//            <option value="1">Audi</option>
//            <option value="2">BMW</option>
//            <option value="3">Mercedes</option>
//            <option value="4">Volvo</option>
//            <option value="5">Lexus</option>
//            <option value="6">Tesla</option>
//          </select>
//        </div>
//        <div class="col-md-4">
//          <select name="radionuclidesclass" id="radionuclidesclass" class="form-select m-1" placeholder="radionuclides">
//            <option value="-1">Radionuclides</option>
//            <option value="1">Audi</option>
//            <option value="2">BMW</option>
//            <option value="3">Mercedes</option>
//            <option value="4">Volvo</option>
//            <option value="5">Lexus</option>
//            <option value="6">Tesla</option>
//          </select>
//        </div>
//        <div class="col-md-4">
//          <select name="accossequipmentsclass" id="accossequipmentclass" class="form-select m-1" placeholder="radionuclides">
//            <option value="-1">Accossiated Equipment</option>
//            <option value="1">Audi</option>
//            <option value="2">BMW</option>
//            <option value="3">Mercedes</option>
//            <option value="4">Volvo</option>
//            <option value="5">Lexus</option>
//            <option value="6">Tesla</option>
//          </select>
//        </div>

        
//        <div class="col-md-6">
//        </div>

//        <div class="col-md-6">
//        </div>
//      </div>
  
//  `);

//  $('#exampleModal').modal('show');






//});


