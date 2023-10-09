import { faListDots, faPen, faGlobe, faArrowAltCircleLeft, faPlus, faXmark, faCircleUser, faHouse, faBrain, faUsersLine, faPersonBurst, faEnvelope } from '@fortawesome/free-solid-svg-icons';
import { DataTableDirective } from 'angular-datatables';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from '../../../services/shared.service';
import { TransactionService } from '../../../../services/TransactionService/TransactionService.service';
import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  forwardRef,
  Inject,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';

import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe, formatDate } from '@angular/common';
import { AppComponent } from 'src/app/app.component';
import { TranslateService } from '@ngx-translate/core';
import { ItemsBilling, ItemsBillingHeader } from '../../../../models/BillingService/ItemsBilling';
@Component({
  selector: 'app-insert-billing-trn-service',
  templateUrl: './insert-billing-trn-service.component.html',
  styleUrls: ['./insert-billing-trn-service.component.css']
})
export class InsertBillingTrnServiceComponent implements OnInit {
  faPlus = faPlus;
  faXmark = faXmark;
  faCircleUser = faCircleUser;
  faHouse = faHouse;
  faBrain = faBrain;
  faPersonBurst = faPersonBurst;
  faUsersLine = faUsersLine;
  faEnvelope = faEnvelope;
  faGlobe = faGlobe;
  faPen = faPen;
  faListDots = faListDots;
  lang: string;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  langServiceSupscribtion: Subscription;
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };
  Practice: any;
  Sector: any;
  Status: any;
  Facility: any;
  PaymentTerms: any;
  ServiceHierarchy: any;
  facilityId: string;
  TotalPrice: number;
  NetAmount: number;
  IdOldInvoice: string;
  Edit: boolean = false;
  View: boolean = false;
  action: string;
  ItemsRow: FormGroup;
  dataList: any;
  roleScreenControls: FormArray;
  items: any;
  Vat: any;
  vatAmount: any;
  deletedItems = [];
  dataFromEdit: string;
  SourceInvoice: string;
  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private sharedService: SharedService,
    private translateService: TranslateService,
    private TransactionService: TransactionService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private changeDetectorRef: ChangeDetectorRef,
  ) { }
  radionuclides: any = [];
  ngOnInit(): void {
    // get language
    this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
      this.lang = l;
      // this.updateOptions();
    });
    this.ItemsRow = this.fb.group({
      tableRows: this.fb.array([]),
    });

    this.roleScreenControls = this.getFormControls;

    this.route.data.subscribe((data) => {
      this.action = data['mode'];
    });

    if (this.route.snapshot.queryParamMap.get('id') != null) {
      this.IdOldInvoice = this.route.snapshot.queryParamMap.get('id');
    }
    if (this.route.snapshot.paramMap.get('action') != null) {
      this.action = this.route.snapshot.paramMap.get('action');
    }

    this.getFacility();
    this.getPaymentTerm();
    this.getStatus();
    this.GetAllSector();
    this.getVat();

    if (this.IdOldInvoice != null) {
      this.ViewOldInvoice();
    } else {
      this.AddNewRowForItemForm();

    }
  }


  AddTrnHeaderData = this.fb.group({
    invoiceCode: [''],
    invoiceDate: [new Date().toISOString().substr(0, 10), [Validators.required]],
    transactionRefNum: ['', [Validators.required]],
    statusFlag: ['1UOzGUqRYajQ1Zmbzzrwnw==', [Validators.required]],
    trnRemarks: [''],
    customerId: ['', [Validators.required]],
    customerName: [''],
    currencyCode: [''],
    exRate: [1.00],
    termsCode: ['', [Validators.required]],
    invoiceSource: ['1'],
    invoiceBU: ['', [Validators.required]],
    sector: ['', [Validators.required]],
  })




  addRow(ItemsBilling: ItemsBilling, index: number) {
    this.getFormControls.insert(index, this.initiateForm(ItemsBilling));
  }
  addHeaeder(ItemsBillingHeader: ItemsBillingHeader) {
    this.AddTrnHeaderData.patchValue({
      invoiceCode: ItemsBillingHeader.invoiceBU,
      invoiceDate: formatDate(ItemsBillingHeader.invoiceDate, 'dd/MM/YYYY', 'en-US'),
      transactionRefNum: ItemsBillingHeader.transactionRefNum,
      statusFlag: ItemsBillingHeader.statusFlag,
      trnRemarks: ItemsBillingHeader.trnRemarks,
      customerId: ItemsBillingHeader.customerId,
      customerName: ItemsBillingHeader.customerName,
      currencyCode: ItemsBillingHeader.currencyCode,
      exRate: 1.00,
      termsCode: ItemsBillingHeader.termsCode,
      invoiceSource: '',
      invoiceBU: ItemsBillingHeader.invoiceBU,
    })
  }
  AddNewRowForItemForm() {
    const controls = this.ItemsRow.get('tableRows') as FormArray;
    var count = controls.length;
    var data: ItemsBilling = {
      lineNum: count + 1,
      serviceItemId: '',
      itemQty: 1,
      itemPrice: 0,
      lineRemarks: '',
      vatIncluded: true,
      vatPcntg: 15,
      vatAmount: 0,
      billableQty: 1,
      TotalpriceItem: 0,
      Unit: 'Each/Piece',
      itmQtyFromService: 0
    }
    this.addRow(data, count);
  }

  ClearForm() {
    
    if (this.Edit != true) {
      this.ServiceHierarchy = [];
      this.ItemsRow.reset();
      this.AddTrnHeaderData.reset();
      this.TotalPrice = 0;
      this.NetAmount = 0;
      this.Practice = [];
      this.vatAmount = 0;
    }
  }
  get getFormControls() {
    const controls = this.ItemsRow.get('tableRows') as FormArray;
    return controls;
  }
  initiateForm(screen: ItemsBilling): FormGroup {
    return this.fb.group({
      lineNum: new FormControl(screen.lineNum),
      serviceItemId: new FormControl(screen.serviceItemId),
      itemQty: new FormControl(screen.itemQty),
      itmQtyFromService: new FormControl(screen.itmQtyFromService),
      itemPrice: new FormControl(screen.itemPrice),
      lineRemarks: new FormControl(screen.lineRemarks),
      vatIncluded: new FormControl(screen.vatIncluded),
      vatPcntg: new FormControl(screen.vatPcntg),
      vatAmount: new FormControl(screen.vatAmount),
      billableQty: new FormControl(screen.billableQty),
      TotalpriceItem: new FormControl(screen.billableQty * screen.itemPrice),
      Unit: new FormControl(screen.Unit),
    });
  }
  assignFormData() {
    this.dataList.forEach((roleScreen, index) => {
      this.addRow(roleScreen, index);
    });
  }


  ViewOldInvoice() {
    if (this.action == "View") {
      this.View = true;
      this.Edit = false;
    } else if (this.action == "Edit") {
      this.View = false;
      this.Edit = true;
    }
    this.getOldInvoice(this.IdOldInvoice);
  }

  getOldInvoice(id: string) {
    this.TransactionService.getOldInvoice(id).subscribe((res) => {
      console.log('getOldInvoice')
      console.log(res)
      this.GetServiceItemByHierarchyCode(res.data.invoiceBU);
      this.GetParentItemStructureCode(res.data.invoiceBU);
      this.addHeaeder(res.data);
      this.dataFromEdit = res.data.invoiceDate;
      this.items = res.data.billingServiceTrnDetails;
      this.SourceInvoice = res.data.invoiceSource;
      if (res.data.statusFlag == 3 || res.data.statusFlag == 4) {
        this.Edit = false;
        this.View = true;
      }
    })

  }
  viewOldItems() {
    for (var x = 0; x < this.items.length; x++) {
      var serviceId = this.items[x].serviceItemId;
      if (this.ServiceHierarchy != null) {
      }
      var obj = this.ServiceHierarchy.filter(_ => _.serviceItemId == serviceId)[0];
      this.addRow(this.items[x], x);
      this.tableRows.controls[x].patchValue({
        itmQtyFromService: obj.itmQty
      })
    }
    this.CalTotalPrice();
  }

  Back() {
    this.router.navigate(['/billing-trn-service'])
  }
  AddTypeItem(index: number, data: any) {
    var service = this.ServiceHierarchy.filter(_ => _.serviceItemId == data.value)[0];
    this.tableRows.controls[index].patchValue({
      itemPrice: service.currentPrice,
      TotalpriceItem: service.currentPrice,
      itmQtyFromService: service.itmQty,
      itemQty: 1,
      billableQty: 1,
      vatPcntg:15
    })
    this.CalTotalPrice();
  }

  get tableRows() {
    return this.ItemsRow.get('tableRows') as FormArray;
  }
  removeRowForItemForm(i: number) {
    if (this.Edit) {
      this.deletedItems.push(this.tableRows.controls[i].value.lineNum);
    }
    this.tableRows.removeAt(i);
    this.CalTotalPrice();
  }

 
  CalTotalPrice() {
    var sum: number = 0;
    var sumIncludeVat: number = 0;
    var vatamount = 0;
    this.TotalPrice = 0;
    this.NetAmount = 0;
    for (var x = 0; x < this.tableRows.controls.length; x++) {
      this.tableRows.controls[x].patchValue({
        vatPcntg: 15
      })
      if (this.tableRows.controls[x].value.vatIncluded) {
        sum = sum + Number(this.tableRows.controls[x].value.TotalpriceItem);
        sumIncludeVat = sumIncludeVat + Number(this.tableRows.controls[x].value.TotalpriceItem + (this.tableRows.controls[x].value.TotalpriceItem * (this.tableRows.controls[x].value.vatPcntg / 100)));
        vatamount = Number(this.tableRows.controls[x].value.TotalpriceItem * (this.Vat / 100))
        this.tableRows.controls[x].patchValue({
          vatAmount: vatamount,
          vatPcntg: 15
        })
      } else {
        sum = sum + Number(this.tableRows.controls[x].value.TotalpriceItem);
        sumIncludeVat = sumIncludeVat + Number(this.tableRows.controls[x].value.TotalpriceItem);
        this.tableRows.controls[x].patchValue({
          vatAmount: 0,
          vatPcntg: 0
        })
      }
    }
    this.TotalPrice = sum;
    this.vatAmount = sumIncludeVat - sum;
    this.NetAmount = sumIncludeVat;
  }
  AddQuantity(index: number, data: any) {
    var itemPrice = this.tableRows.controls[index].value.itemPrice;
    var Bqty = this.tableRows.controls[index].value.itmQtyFromService;
    var BillableQty = 1;
    if (Bqty == 0) {
      Bqty = 1;
    }
    if (data.value > Bqty && Bqty != 0) {
      BillableQty = Number(data.value / Bqty)
    }
    BillableQty = Math.ceil(BillableQty);
    this.tableRows.controls[index].patchValue({
      billableQty: BillableQty,
      TotalpriceItem: itemPrice * BillableQty,
    })
    this.CalTotalPrice();
  }
  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }
  GetAllSector() {
    this.TransactionService.GetAllSector().subscribe((res) => {
      this.Sector = res['data'];
    })
  }
  GetParentItemStructureCode(data: any) {
    this.TransactionService.GetParentItemStructureCode(data).subscribe((res) => {
      this.AddTrnHeaderData.patchValue({
        sector: res.data.parentStructure
      })
      this.getPractice(res.data.parentStructure);
    })
  }
  getPractice(data: any) {
    this.TransactionService.GetAllPractice(data).subscribe((res) => {
      this.Practice = res['data'];
    })
    this.ServiceHierarchy = [];
    for (var x = 0; x < this.tableRows.controls.length; x++) {
      this.tableRows.controls[x].patchValue({
        itemPrice: 0,
        TotalpriceItem: 0,
        itmQtyFromService: 0,
        itemQty: 1,
        billableQty: 1,
        vatPcntg: 15
      })
    }
  }
  getStatus() {
    this.TransactionService.GetStatus().subscribe((res) => {
      this.Status = res.data.lookupSetTerms;
    })
  }
  getFacility() {
    this.TransactionService.GetFacility().subscribe((res) => {
      console.log("GetFacility");
      console.log(res);
      this.Facility = res.data;
    })
  }
  getPaymentTerm() {
    this.TransactionService.GetPaymentTerm().subscribe((res) => {
      console.log("GetPaymentTerm");
      console.log(res);
      this.PaymentTerms = res.data.lookupSetTerms;
    })
  }
  GetServiceItemByHierarchyCode(Code: string) {
    this.TransactionService.GetServiceItemByHierarchyCode(Code).subscribe((res) => {
      this.ServiceHierarchy = res.data;
      if (this.View || this.Edit) {
        this.viewOldItems();
      }
      
    })
    for (var x = 0; x < this.tableRows.controls.length; x++) {
      this.tableRows.controls[x].patchValue({
        itemPrice: 0,
        TotalpriceItem: 0,
        itmQtyFromService: 0,
        itemQty: 1,
        billableQty: 1,
        vatPcntg: 15
      })
    }
  }

  getVat() {
    this.TransactionService.GetVat().subscribe((res) => {
      this.Vat = Number(res.data.lookupSetTerms[0].extraData1);
      console.log(res.data.lookupSetTerms[0]);
    })
  }
  SelectedFacility(data) {
    this.AddTrnHeaderData.patchValue({
      customerId: data
    })
  }


  SubmitForm() {

    this.AddTrnHeaderData.markAllAsTouched();
    debugger
    if (this.AddTrnHeaderData.valid) {
      var Custname = this.Facility.filter(_ => _.refCode == this.AddTrnHeaderData.value.customerId)[0].customerNameAr;
      var InvoceCode = this.AddTrnHeaderData.controls.invoiceBU.value;
      this.AddTrnHeaderData.patchValue({
        customerName: Custname,
        invoiceCode: InvoceCode
      })
      var resultDetails = this.tableRows.controls;
      var details = [];
      resultDetails.forEach((items, index) => {
        details[index] = items.value;
      });
      var resultHeader = { ...this.AddTrnHeaderData.value, BillingServiceTrnDetails: details };

      var body = { header: resultHeader };
      if (this.Edit == true) {
        resultHeader.invoiceDate = this.dataFromEdit
        resultHeader.invoiceSource = this.SourceInvoice
        var obj = { header: resultHeader, TransActionID: this.IdOldInvoice, deletedItems: this.deletedItems };
        console.log(obj);
        this.TransactionService.Update(obj).subscribe((res) => {
          if (res.succeeded) {
            this.app.messageService.add({
              severity: 'success',
              key: 'PlanValidation',
              summary: this.translateService.instant('Saved As Draft'),
              detail: this.translateService.instant('Saved As Draft'),
              life: 6000,
            });
            this.Back();
          }
        })
      } else {
        this.TransactionService.SaveData(body).subscribe((res) => {
          if (res.succeeded) {
            this.app.messageService.add({
              severity: 'success',
              key: 'PlanValidation',
              summary: this.translateService.instant('Saved As Draft'),
              detail: this.translateService.instant('Saved As Draft'),
              life: 6000,
            });
            this.Back();
          }
        })
      }
      /*this.TotalPrice = 0;
      this.NetAmount = 0;
      this.Practice = [];
      //this.PaymentTerms = [];
      this.AddTrnHeaderData.reset();
      this.ItemsRow.reset();*/
    }
  }



  dtInstanceObj: any;
  initialized: boolean = false;
}








