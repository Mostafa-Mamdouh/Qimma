import {
  Component,
  forwardRef,
  Inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subject, Subscription } from 'rxjs';
import { NuclearMaterialServices } from '../../../../services/NuclearMaterial/nuclear-material.service';
import { SharedService } from '../../../services/shared.service';
import {
  faTrash,
  faArrowAltCircleLeft,
  faPen,
  faEye,
} from '@fortawesome/free-solid-svg-icons';
import { DataTableDirective } from 'angular-datatables';
import { AppComponent } from '../../../app.component';
import { UserInfoServices } from '../../../../services/userinfo.service';
import UserData from '../../../../models/Common/userdata';
@Component({
  selector: 'app-nuclear-materials',
  templateUrl: './nuclear-materials.component.html',
  styleUrls: ['./nuclear-materials.component.css'],
})
export class NuclearMaterialsComponent implements OnInit {
  // Datatable
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };
  dtInstanceObj: any;
  initialized: boolean = false;

  // Language
  lang: string;
  langServiceSupscribtion: Subscription;

  // Icons
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  faPen = faPen;
  faTrash = faTrash;
  faEye = faEye;

  // Data
  DataList: any[] = [];

  // Current User
  currentUser: UserData = <UserData>{};

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private nuclearMaterialServices: NuclearMaterialServices,
    private sharedService: SharedService,
    private userService: UserInfoServices,
    private router: Router
  ) {}

  ngOnInit(): void {
    // get language
    this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
      this.lang = l;
    });

    this.getUserData();
  }

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

  getUserData(): void {
    this.currentUser = this.userService.getCurrentUser();
    this.getData();
  }

  showMaterialTypeInTable(types) {
    if (types.length > 0) {
      var typeNames = types.map(function (type) {
        let x = '';
        if (type.element == 'd') {
          x = 'DU';
        }
        if (type.element == 'e') {
          x = 'EU';
        }
        if (type.element == 'n') {
          x = 'NU';
        }
        if (type.element == 'pu') {
          x = 'Pu';
        }
        if (type.element == 'th') {
          x = 'Th';
        }
        return x;
      });
      if (typeNames.length == 0) {
        return '---';
      }
      if (typeNames.length == 1) {
        return typeNames[0];
      }
      return typeNames.join(', ');
    }
  }

  getData() {
    this.dtOptions = {
      paging: true,
      pagingType: 'full_numbers',
      pageLength: 10,
      autoWidth: true,
      serverSide: true,
      searching: true,
      ajax: (dataTablesParameters: any, callback, settings) => {
        console.log(dataTablesParameters);
        dataTablesParameters.searchCriteria = this._searchCriteria;
        this.nuclearMaterialServices
          .getNuclearMaterialsFunctional(dataTablesParameters)
          .subscribe((resp) => {
            if (resp['succeeded']) {
              console.log('resp: ', resp);
              this.dtTrigger.next();
              this.DataList = resp['data'];
              callback({
                recordsTotal: resp['recordsTotal'],
                recordsFiltered: resp['recordsFiltered'],
                data: resp['data'],
              });
            } else {
              this.app.messageService.add({
                severity: 'error',
                key: 'PlanValidation',
                summary: this.translateService.instant('General.Error'),
                detail:
                  this.translateService.instant('General.Error') +
                  ' ' +
                  resp['errors'].join('=>'),
                life: 6000,
              });
            }
          });
      },
      columns: [
        { data: 'nrrcId' },
        { data: 'manufacturerSerialNo' },
        { data: 'sourceModel' },
        { data: this.lang == 'ar' ? 'facilityAr' : 'facilityEn' },
        { data: this.lang == 'ar' ? 'physicalFormAr' : 'physicalFormEn' },
        { data: 'chemicalForm' },
        {
          data:
            this.lang == 'ar'
              ? 'nuclearMaterialTypeAr'
              : 'nuclearMaterialTypeEn',
        },
        { data: this.lang == 'ar' ? 'statusAr' : 'statusEn' },
        { data: 'isShield' },
        { data: null },
      ],
    };
  }

  /* Add Nuclear Material */
  addNucleaMaterial(): void {
    this.router.navigate(['nuclear-material-form']);
  }
  /* Update Nuclear Material */
  updateNucleaMaterial(id: string): void {
    this.router.navigate(['nuclear-material-form'], {
      queryParams: { id: id },
    });
  }
  /* view Nuclear Material */
  viewNucleaMaterial(id: string): void {
    this.router.navigate(['nuclear-material-form'], {
      queryParams: { id: id, isView: true },
    });
  }
  /* Delete Nuclear Material */
  deleteNucleaMaterial(id: string, nrrcId: string): void {
    let _this = this;
    console.log('id: ', id);
    this.app.confirmationService.confirm({
      message:
        _this.translateService.instant('Confirmation.deleteMsg') +
        ': ' +
        nrrcId,
      header: _this.translateService.instant('Confirmation.confirm'),
      acceptLabel: _this.translateService.instant('Confirmation.Yes'),
      rejectLabel: _this.translateService.instant('Confirmation.No'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        _this.nuclearMaterialServices
          .deleteNuclearMaterial(id)
          .subscribe(function (response) {
            if (response['succeeded']) {
              console.log(response);
              if (response['data']) {
                _this.reload();
                _this.app.messageService.add({
                  severity: 'success',
                  key: 'PlanValidation',
                  summary: _this.translateService.instant(
                    'General.DeletedSuccessfully'
                  ),
                  detail: _this.translateService.instant(
                    'General.DeletedSuccessfully'
                  ),
                  life: 6000,
                });
              } else {
                _this.app.messageService.add({
                  severity: 'warn',
                  key: 'PlanValidation',
                  summary: _this.translateService.instant('General.CascadeMsg'),
                  detail: _this.translateService.instant('General.CascadeMsg'),
                  life: 6000,
                });
              }
            } else {
              _this.app.messageService.add({
                severity: 'error',
                key: 'PlanValidation',
                summary: _this.translateService.instant('General.Error'),
                detail:
                  _this.translateService.instant('General.Error') +
                  ' ' +
                  response['errors'].join('=>'),
                life: 6000,
              });
            }
          });
      },
      reject: () => {
        _this.app.messageService.add({
          severity: 'warn',
          key: 'PlanValidation',
          summary: _this.translateService.instant('General.DeleteCancelled'),
          detail: _this.translateService.instant('General.DeleteCancelled'),
          life: 6000,
        });
      },
    });
  }

  /* Navigation Functions */
  Breadcrumb(rValue: string) {
    this.router.navigate([rValue]);
  }

  Back() {
    this.router.navigate(['']);
  }

  // DataTable Methods
  reload() {
    var _this = this;
    _this.getData();
    _this.rerender();
  }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      this.dtTrigger.next();
      dtInstance.ajax.reload();
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
}
