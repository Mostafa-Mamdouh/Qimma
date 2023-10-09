import { Component, forwardRef, OnInit, Inject } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { faPlus, faXmark } from '@fortawesome/free-solid-svg-icons';
import { Subscription } from 'rxjs';
import { ShieldingMaterial } from 'src/app/Enumerations/shieldingMaterials';
import { SourceTypes } from 'src/app/Enumerations/sourceTypes';
import { DateLessThanToday } from 'src/app/helper/validations/DateLessThanToday';
import { AddNewSourceModel } from 'src/app/models/addNewSourceModel/addNewSourceModel';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { InsertNewRecordService } from './../../../services/Insert-New-Recored/insert-new-record.service';
import { AttachmentsTypes } from 'src/app/Enumerations/attachments';
import { currentActivty } from 'src/app/helper/calculations';
import { PdfToBase64 } from 'src/app/helper/fileValidation';
import { AppSettings } from 'src/app/AppSettings';
import { AppComponent } from 'src/app/app.component';
import { TranslateService } from '@ngx-translate/core';
import { TransactionTypes } from 'src/app/Enumerations/TransactionTypes';
import { ActivatedRoute, Router } from '@angular/router';
import { TransactionService } from 'src/app/services/TransactionService/TransactionService.service';
declare var $: any;

@Component({
  selector: 'app-add-new-generator',
  templateUrl: './add-new-generator.component.html',
  styleUrls: ['./add-new-generator.component.css'],
})
export class AddNewGeneratorComponent implements OnInit {
  updateId: string;
  isUpdate: boolean = false;
  isTransfer: boolean = false;

  // Object
  source: any;

  // Icons
  faPlus = faPlus;
  faXmark = faXmark;

  // Enumerations
  AttachmentsTypes = AttachmentsTypes;
  TransactionTypes = TransactionTypes;

  // Language
  lang: string;
  langServiceSupscribtion: Subscription;

  // Lookups and DropDowns
  lookups?: any = {};
  statusLookup?: any[];
  entities?: any[] = [];
  facilities?: any[] = [];
  licenses?: any[] = [];
  addTypes: any[] = [
    {
      id: TransactionTypes.NewSourceFromImport,
      name: 'Import',
    },
    {
      id: TransactionTypes.NewSourceFromTransfer,
      name: 'Transfer',
    },
  ];

  // Disable Dates after today
  currentDate: any = new Date();

  //Is Digonostic Or Therapeutic
  isDigonostic = false;
  isTherapeutic = false;

  // Source Variables and Attachments
  sourceFiles: File[] = [];

  manufacturerCertificates: File[] = [];
  customImportPermits: File[] = [];
  shipperImportPermits: File[] = [];
  customExportPermits: File[] = [];
  shipperExportPermits: File[] = [];
  otherDocumnets: File[] = [];

  isSourceManufacturerCertificateValid: boolean = true;
  isSourceCustomImportPermitsValid: boolean = true;
  isSourceShipperImportPermitsValid: boolean = true;

  // Attachments
  selectedSourceAttachments?: any[] = [];
  selectedMaterialAttachments?: any[] = [];
  isSourceAttachmentValid: boolean = true;

  // Form Validation Variables
  submitBtnClicked: boolean = false;

  // Form
  addNewSourceForm = this.fb.group({
    entity: ['', Validators.required],
    facility: ['', Validators.required],
    licenseNumber: ['', Validators.required],
    manufacturer: ['', Validators.required],
    manufacturerSerialNo: ['', Validators.required],
    purpose: ['', Validators.required],
    status: ['', Validators.required],
    facilityGeneratorID: [''],
    dateOfManufacturing: [null, [Validators.required, DateLessThanToday]],
    equipmentType: ['', Validators.required],
    xRayTubeSerialNo: [''],
    tubeMaximumEnergy: [''],
    tubeMaximumEnergyUnit: [''],
    tubeMaximumCurrent: [''],
    tubeMaximumCurrentUnit: [''],
    maximumEnergy: ['', Validators.required],
    maximumEnergyUnit: ['', Validators.required],
    maximumDoseRate: [''],
    maximumDoseRateAt01m: [''],
    maximumCurrent: ['', Validators.required],
    maximumCurrentUnit: ['', Validators.required],
    doseEquivalentType: ['', Validators.required],
    doseRate: ['', Validators.required],
    exemption: ['', Validators.required],
    modelNumber: ['', Validators.required],
    shieldMaterial: ['', Validators.required],
    shieldNuclearMaterialCode: [''],
  });

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private sharedService: SharedService,
    private fb: FormBuilder,
    private insertNewRecordService: InsertNewRecordService,
    private translateService: TranslateService,
    private router: Router,
    private transactionService: TransactionService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // get language
    this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
      this.lang = l;
    });

    // get current entity and facility
    if (!this.updateId) {
      this.sharedService.currentEntity.subscribe((entity) => {
        console.log(entity);
        if (!entity) {
          // this.router.navigate(['/home']);
        }
        this.addNewSourceForm.controls['entity'].setValue(entity);
      });

      this.sharedService.currentFacility.subscribe((facility) => {
        console.log(facility);
        if (!facility) {
          // this.router.navigate(['/home']);
        }
        this.addNewSourceForm.controls['facility'].setValue(facility);
        this.getLicensesByFacilityId(facility.id);
      });

      this.sharedService.currentLicense.subscribe((license) => {
        this.addNewSourceForm.controls['licenseNumber'].setValue(license);
        if (!license) {
          // this.router.navigate(['/home']);
        }
      });
    }

    // get lookups
    this.insertNewRecordService.getSourceFormsLookup().subscribe((res) => {
      res['data'].forEach((element) => {
        this.lookups[element['className']] = element['lookupSetTerms'];
      });
      console.log(this.lookups);
    });

    // get entities
    this.insertNewRecordService.getEntities().subscribe((res) => {
      this.entities = res['data'];
    });
  }

  // Get Facilities by Entity
  getFacilitiesByEntityId(id: string, updateIds?: number[]) {
    this.insertNewRecordService.getFacilities(id).subscribe((res) => {
      this.facilities = res['data'];
      if (this.isUpdate) {
        let facility = this.facilities.find(
          (x) => x.facilityId == updateIds[0]
        );
        this.addNewSourceForm.controls['facility'].setValue(facility);
        this.getLicensesByFacilityId(facility.id, updateIds);
      }
    });
  }
  // Get Licenses by Facility
  getLicensesByFacilityId(id: string, updateIds?: number[]) {
    this.insertNewRecordService.getLicenses(id).subscribe((res) => {
      this.licenses = res['data'];
      if (this.isUpdate) {
        let license = this.licenses.find((x) => x.licenseId == updateIds[0]);
        if (license) {
          this.addNewSourceForm.controls['licenseNumber'].setValue(license);
        }
      }
    });
  }

  handleXRayTubeChange(e) {}

  // on device type change
  onDeviceTypeChange() {
    console.log('onDeviceTypeChange');
    this.isDigonostic = false;
    this.isTherapeutic = false;
    let typeId = this.addNewSourceForm.get('equipmentType').value;
    let type = this.lookups['EquipmentType'].find(
      (x) => x.id == typeId['id']
    ).extraData1;
    console.log(typeId);
    if (type == 'diagnostic') {
      this.isDigonostic = true;
      this.addNewSourceForm.controls['xRayTubeSerialNo'].enable();
      this.addNewSourceForm.controls['tubeMaximumEnergy'].enable();
      this.addNewSourceForm.controls['tubeMaximumEnergyUnit'].enable();
      this.addNewSourceForm.controls['tubeMaximumCurrent'].enable();
      this.addNewSourceForm.controls['tubeMaximumCurrentUnit'].enable();
      this.addNewSourceForm.controls['maximumDoseRate'].disable();
      this.addNewSourceForm.controls['maximumDoseRateAt01m'].disable();
    } else if (type == 'therapeutic') {
      this.isTherapeutic = true;
      this.addNewSourceForm.controls['maximumDoseRate'].enable();
      this.addNewSourceForm.controls['maximumDoseRateAt01m'].enable();
      this.addNewSourceForm.controls['xRayTubeSerialNo'].disable();
      this.addNewSourceForm.controls['tubeMaximumEnergy'].disable();
      this.addNewSourceForm.controls['tubeMaximumEnergyUnit'].disable();
      this.addNewSourceForm.controls['tubeMaximumCurrent'].disable();
      this.addNewSourceForm.controls['tubeMaximumCurrentUnit'].disable();
    } else {
      this.addNewSourceForm.controls['xRayTubeSerialNo'].disable();
      this.addNewSourceForm.controls['tubeMaximumEnergy'].disable();
      this.addNewSourceForm.controls['tubeMaximumEnergyUnit'].disable();
      this.addNewSourceForm.controls['tubeMaximumCurrent'].disable();
      this.addNewSourceForm.controls['tubeMaximumCurrentUnit'].disable();
      this.addNewSourceForm.controls['maximumDoseRate'].disable();
      this.addNewSourceForm.controls['maximumDoseRateAt01m'].disable();
    }
  }

  // Is Exempted
  isExempted() {
    let maximumEnergy = this.addNewSourceForm.get('maximumEnergy').value;
    let maximumEnergyUnitId =
      this.addNewSourceForm.get('maximumEnergyUnit').value;
    let doseRate = this.addNewSourceForm.get('doseRate').value;
    let maximumEnergyUnit;
    if (maximumEnergyUnitId) {
      maximumEnergyUnit = this.lookups['EnergyUnit'].find(
        (x) => x.id == maximumEnergyUnitId['id']
      );
    }
    if (maximumEnergy && maximumEnergyUnit && doseRate) {
      if (
        (maximumEnergyUnit.value == 'keV' && parseFloat(maximumEnergy) < 5) ||
        parseFloat(doseRate) < 1
      ) {
        let exm = this.lookups['Boolean'].find((x) => x.value == '1');
        this.addNewSourceForm.get('exemption').patchValue(exm);
      } else {
        let exm = this.lookups['Boolean'].find((x) => x.value == '0');
        this.addNewSourceForm.get('exemption').patchValue(exm);
      }
    }
  }

  submitNewSource() {
    this.submitBtnClicked = true;
    // let attachmentsStatus = this.validateAttachments();
    let body = this.addNewSourceForm.value;

    if (this.addNewSourceForm.valid) {
      console.log(body);
      this.app.messageService.add({
        severity: 'success',
        key: 'PlanValidation',
        summary: this.translateService.instant('submitedSuccessfully'),
        detail: this.translateService.instant('submitedSuccessfully'),
        life: 6000,
      });
    } else {
      this.app.messageService.add({
        severity: 'error',
        key: 'PlanValidation',
        summary: 'Error',
        detail: this.translateService.instant('validationMessage'),
        life: 6000,
      });
    }
  }
  SaveAsDraft() {
    let body = this.addNewSourceForm.value;
    console.log('body', body);
    this.app.messageService.add({
      severity: 'success',
      key: 'PlanValidation',
      summary: this.translateService.instant('savedAsDraft'),
      detail: this.translateService.instant('savedAsDraft'),
      life: 6000,
    });
  }
}
