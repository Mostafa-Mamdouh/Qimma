import { Component, forwardRef, Inject, ViewChild, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faTrash, faPenToSquare, faListDots, faArrowAltCircleLeft } from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, Validators } from '@angular/forms';
import { AppComponent } from '../../app.component';
import { UserInfoServices } from '../../../services/userinfo.service';
import UserData from '../../../models/Common/userdata';
import { Response } from '../../../models/Common/response';
import { FormControl } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from '../../services/shared.service';
import { CommonClass } from '../../common/CommonClass';
import { Title } from "@angular/platform-browser";
import { DataTableDirective } from 'angular-datatables';
import { ClipboardService } from 'ngx-clipboard';
import { RoleService } from '../../../services/InternalRole/role.service';
declare var $: any;

@Component({
  selector: 'role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})

export class RoleComponent implements OnInit {
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };

  currentUser: UserData = <UserData>{};
  roles = [];
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  update = 0;

  roleId: string;
  roleNameAr: string = '';
  roleNameEn: string = '';
  roleCode: string = '';

  faTrash = faTrash;
  faPen = faPenToSquare;
  faListDots = faListDots;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  preview: string = '';

  RoleForm = new FormGroup({
    RoleNameAr: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    RoleNameEn: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    RoleCode: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
  });

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private roleService: RoleService,
    private userService: UserInfoServices,
    private router: Router,
    private route: ActivatedRoute,
    private sharedService: SharedService,
    private commonClass: CommonClass,
    private titleService: Title,
    private clipboardApi: ClipboardService) {
    this.route.queryParams.subscribe(params => { });
  }

  ngOnInit(): void {
    this.getUserData();
    this.langServiceSupscribtion = this.sharedService.lang.subscribe(
      (l) => (this.lang = l)
    );
  }
  dtInstanceObj: any;
  initialized: boolean = false;
  ngAfterViewInit(): void {
    this.translateService.stream('ApplicationTitle').subscribe(value => {
      this.translateService.stream('Role.PageTitle').subscribe(valueTitle => {
        this.titleService.setTitle(value + valueTitle);
      });
    });
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
    var _this = this;
    _this.currentUser=_this.userService.getCurrentUser();
    _this.getAllrole();
  }
  isNumberKey(evt: any) {
    return this.commonClass.isNumberKey(evt);
  }
  OnlyArabic(event: any, lng: any) {
    return this.commonClass.OnlyArabic(event, lng);
  }
  OnlyEnglish(event: any, lng: any) {
    return this.commonClass.OnlyEnglish(event, lng);
  }
  OnlyEnglishCode(event: any, lng: any) {
    return this.commonClass.OnlyEnglishCode(event, lng);
  }
  Delete(obj: any) {
    var _this = this;
    _this.reset();
    _this.app.confirmationService.confirm({
      message: _this.translateService.instant('Confirmation.deleteMsg') + ": " + obj.roleCode,
      header: _this.translateService.instant('Confirmation.confirm'),
      acceptLabel: _this.translateService.instant('Confirmation.Yes'),
      rejectLabel: _this.translateService.instant('Confirmation.No'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {

        _this.roleService.DeleteRole(obj.id).subscribe(function (data) {
          var response = data as Response<any>;
          if (response.succeeded) {
            let index = _this.roles.findIndex(d => d.roleId === obj.roleId);
            _this.roles.splice(index, 1);
            _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.DeletedSuccessfully'), detail: _this.translateService.instant('General.DeletedSuccessfully'), life: 6000 });
          }
          else {
            _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
          }
        })
      },
      reject: () => {
        _this.app.messageService.add({ severity: 'warn', key: 'PlanValidation', summary: _this.translateService.instant('General.DeleteCancelled'), detail: _this.translateService.instant('General.DeleteCancelled'), life: 6000 });
      }
    })

  }
  getAllrole() {
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
        this.roleService
          .GetAllRolesFunctional(dataTablesParameters)
          .subscribe((resp) => {
            if (resp['succeeded']) {
              this.dtTrigger.next();
              this.roles = resp['data'];
              callback({
                recordsTotal: resp['recordsTotal'],
                recordsFiltered: resp['recordsFiltered'],
                data: resp['data'],
              });
            } else {
              this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: this.translateService.instant('General.Error'), detail: this.translateService.instant('General.Error') + " " + resp['errors'].join('=>'), life: 6000 });
            }
          });
      },
      columns: [
        { data: 'roleNameAr' },
        { data: 'roleNameEn' },
        { data: 'roleCode' },
        { title: 'actions', data: null }
      ],
    };
  }
  reset() {
    this.update = 0;
    this.RoleForm.reset();
  }
  Details(obj: any) {
    this.roleId = obj.id;
    this.roleCode = obj.roleCode;
    this.roleNameAr = obj.roleNameAr;
    this.roleNameEn = obj.roleNameEn;
    this.update = 1;
  }
  save() {
    var lang = this.translateService.currentLang;
    if (this.update == 0) {
      if (this.RoleForm.valid) {
        this.preview = JSON.stringify(this.RoleForm.value);
        var _this = this;
        _this.roleService.AddRole(this.preview).subscribe(function (data) {
          var response = data as Response<any>;
          if (response.succeeded) {
            _this.roles.push(response.data)
            _this.reset();
            _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.SavedSuccessfully'), detail: _this.translateService.instant('General.SavedSuccessfully'), life: 6000 });
          } else {
            _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
          }
        })
      }
      else {
        this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: this.translateService.instant('General.RequiredAllFields'), detail: this.translateService.instant('General.RequiredAllFields'), life: 6000 });
      }
    }
    else {
      var obj = {
        "id": this.roleId,
        "roleCode": this.roleCode,
        "roleNameAr": this.roleNameAr,
        "roleNameEn": this.roleNameEn,

      }
      var _this = this;
      _this.roleService.UpdateRole(obj).subscribe(function (data) {
        var response = data as Response<any>;
        if (response.succeeded) {

          _this.roles.map((obj) => {
            if (obj.roleId === response.data.roleId) {
              obj.roleCode = response.data.roleCode;
              obj.roleNameAr = response.data.roleNameAr;
              obj.roleNameEn = response.data.roleNameEn;
            }
          });

          _this.reset();
          _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.SavedSuccessfully'), detail: _this.translateService.instant('General.SavedSuccessfully'), life: 6000 });
        } else {
          _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
        }
      })
    }
  }



 

  Back() {
    this.router.navigate(['internal-user-settings']);
  }


  // DataTable Methods
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
  Home() {
    this.router.navigate(['aman-settings']);
  }
  GotoRoleScreensPage(obj: any) {

    this.router.navigate(['role-screens'], { queryParams: { Id: obj.id } });
  }
}
