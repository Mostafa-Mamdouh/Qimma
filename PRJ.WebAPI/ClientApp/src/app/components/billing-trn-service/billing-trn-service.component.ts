import { Router } from '@angular/router';
import { faListDots,faPen,faGlobe, faPlus, faXmark ,faCircleUser,faHouse, faBrain,faUsersLine,faPersonBurst,faEnvelope} from '@fortawesome/free-solid-svg-icons';
import { DataTableDirective } from 'angular-datatables';
import { WorkerService } from 'src/app/services/Worker/worker.service';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { TransactionService } from 'src/app/services/TransactionService/TransactionService.service';

import {
  ChangeDetectorRef,
  Component,
  Inject,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { InsertBillingTrnServiceComponent } from './insert-billing-trn-service/insert-billing-trn-service.component';
@Component({
  selector: 'app-billing-trn-service',
  templateUrl: './billing-trn-service.component.html',
  styleUrls: ['./billing-trn-service.component.css']
})
export class BillingTrnServiceComponent implements OnInit {
  @ViewChild('action') action: InsertBillingTrnServiceComponent;
  faPlus = faPlus;
  faXmark = faXmark;
  faCircleUser =faCircleUser;
  faHouse=faHouse;
  faBrain=faBrain;
  faPersonBurst=faPersonBurst;
  faUsersLine=faUsersLine;
  faEnvelope=faEnvelope;
  faGlobe=faGlobe;
  faPen = faPen;
  faListDots =faListDots;
  lang: string;
  langServiceSupscribtion: Subscription;
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  Status:any;
  private _searchCriteria: any = { isPageLoad: true, filter: '' };
  constructor( 
    private sharedService: SharedService,
    private workerService :WorkerService,
    private TransactionService:TransactionService,
    private fb: FormBuilder,
    private router: Router
    ) { }
    radionuclides: any = [];
    ngOnInit(): void {
      // get language
      this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
        this.lang = l;
        // this.updateOptions();
      });
      this.getdata();
      this.getStatus();
      this.GetServiceItemByHierarchyCode("ss");
    }

    get ItemsForm() {
      return this.AddTrnHeaderData.get('ItemsForm') as FormArray;
    }
AddTrnHeaderData = this.fb.group({
ItemsForm : this.fb.array([
      this.fb.group({
        radionuclide: ['', Validators.required],
        initialActivity: [null],
        initialActivity2: [null],
        initialActivity4: [null],
        activityUnit1: ['']
      })
    ])
});
    addRadionuclides() {
      this.ItemsForm.push(
        this.fb.group({
          radionuclide: [''],
          initialActivity: [''],
          activityUnit: [''],
        })
      );
    }
    removeRadionuclides(i: number) {
      this.ItemsForm.removeAt(i);
    }
 
 
GoToView(id:any){
this.action.action = "View";
this.action.IdOldInvoice =id;
this.router.navigate(['/InsertBillnigTrn'])
}
GoToEdit(id : any){
  this.action.action = "Edit";
  this.action.IdOldInvoice =id;
  this.router.navigate(['/InsertBillnigTrn'])
  }
GetServiceItemByHierarchyCode(Code : string){
  this.TransactionService.GetServiceItemByHierarchyCode("101005").subscribe((res) => {
console.log(res);
  })
  }

  getStatus(){
    this.TransactionService.GetStatus().subscribe((res) => {
      this.Status = res.data.lookupSetTerms;
      console.log(this.Status);
        })
  }

getdata(){
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
      this.TransactionService.GetAllFunction(dataTablesParameters)
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
      { data :'transactionRefNum'},
      { data: 'invoiceDate' },
      { data: 'invoiceBU' },
      { data: 'customerName' },
      { data: 'invoiceSource' },
      { data: 'statusFlag' },
      { title:'actions', data: null }
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

ToEditInvoice(id:any){
  this.router.navigate(['/BillnigTrnEdit'] ,
    {
      queryParams : {id : id}
    })
}
ToViewInvoice(id:any){
  this.router.navigate(['/BillnigTrnView'] ,
    {
      queryParams : {id : id}
    })
}
 
}
