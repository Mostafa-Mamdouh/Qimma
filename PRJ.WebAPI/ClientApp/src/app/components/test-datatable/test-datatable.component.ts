import { Component, OnInit, ViewChild } from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { InsertNewRecordService } from './../../services/Insert-New-Recored/insert-new-record.service';

@Component({
  selector: 'app-test-datatable',
  templateUrl: './test-datatable.component.html',
  styleUrls: ['./test-datatable.component.css'],
})
export class TestDatatableComponent implements OnInit {
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };

  radionuclides: any = [];

  constructor(private service: InsertNewRecordService) {}

  ngOnInit(): void {
    this.dtOptions = {
      paging: true,
      pagingType: 'full_numbers',
      pageLength: 10,
      autoWidth: true,
      serverSide: true,
      processing: true,
      searching: true,
      ajax: (dataTablesParameters: any, callback, settings) => {
        console.log('dataTablesParameters:', dataTablesParameters);
        dataTablesParameters.searchCriteria = this._searchCriteria;
        this.service
          .getRadionulicdesFunctional(dataTablesParameters)
          .subscribe((resp) => {
            console.log('resp:', resp);
            this.dtTrigger.next();
            this.radionuclides = resp['data'];
            callback({
              recordsTotal: resp['recordsTotal'],
              recordsFiltered: resp['recordsFiltered'],
              data: resp['data'],
            });
          });
      },
      columns: [
        { data: 'radionuclideId' },
        { data: 'isotop' },
        { data: 'displayName' },
        { data: 'dValue' },
        { data: 'dValueUnit' },
        { data: 'halfLife' },
        { data: 'halfLifeUnit' },
        { data: 'exemptionValue' },
        { data: 'exemptionValueUnit' },
        { data: 'isActive' },
      ],
    };
  }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      this.dtTrigger.next();
    });
  }

  search() {
    this.rerender();
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  private refreshData(): void {
    this.rerender();
    this.subscribeToData();
  }

  private subscribeToData(): void {
    this.refreshData();
  }

  dtInstanceObj: any;
  initialized: boolean = false;
  ngAfterViewInit(): void {
    var me = this;
    this.dtTrigger.subscribe(() => {
      this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
        if (!this.initialized) {
          this.initialized = true;
          me.dtInstanceObj = dtInstance;
          dtInstance.columns().every(function (e) {
            const that = this;
            var counter = -1;
            $('.search-input').each(function () {
              counter += 1;
              if (counter == e) {
                $(this).on('keyup change', function () {
                  var value = $(this).val();
                  if (that.search() !== value) {
                    that.search(value as string).draw();
                  }
                });
              }
            });
          });
        }
      });
    });
  }
}
