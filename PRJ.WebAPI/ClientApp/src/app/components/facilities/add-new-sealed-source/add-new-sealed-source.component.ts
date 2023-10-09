import { Component, forwardRef, OnInit, Inject } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { faPlus, faXmark } from '@fortawesome/free-solid-svg-icons';
import { Subscription } from 'rxjs';
import { ShieldingMaterial } from 'src/app/Enumerations/shieldingMaterials';
import { SourceTypes, SourceStatus } from 'src/app/Enumerations/sourceTypes';
import { DateLessThanToday } from 'src/app/helper/validations/DateLessThanToday';
import { AddNewSourceModel } from 'src/app/models/addNewSourceModel/addNewSourceModel';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { InsertNewRecordService } from './../../../services/Insert-New-Recored/insert-new-record.service';
import { AttachmentsTypes } from 'src/app/Enumerations/attachments';
import { currentActivty } from 'src/app/helper/calculations';
import { Base64ToPdf, PdfToBase64 } from 'src/app/helper/fileValidation';
import { TranslateService } from '@ngx-translate/core';
import { AppComponent } from 'src/app/app.component';
import { TransactionTypes } from 'src/app/Enumerations/TransactionTypes';
import { ItemSourcesProfileService } from 'src/app/services/ItemSourcesProfile/item-sources-profile.service';
import { NuclearMaterialServices } from 'src/app/services/nuclearMaterial/nuclear-material.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LoaderService } from 'src/app/services/General/loader.service';
import { formatDate } from '@angular/common';
import { ConfirmationService } from 'primeng/api';
import { TransactionService } from 'src/app/services/TransactionService/TransactionService.service';
declare var $: any;

@Component({
  selector: 'app-add-new-sealed-source',
  templateUrl: './add-new-sealed-source.component.html',
  styleUrls: ['./add-new-sealed-source.component.css'],
})
export class AddNewSealedSourceComponent implements OnInit {
  shieldCodeList: any[] = [];
  updateId: string;
  isUpdate: boolean = false;
  isTransfer: boolean = false;
  // Object
  source: any;
  // Icons
  faPlus = faPlus;
  faXmark = faXmark;

  // Enumerations
  SourceTypes = SourceTypes;
  ShieldingMaterial = ShieldingMaterial;
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
  permits?: any[] = [];
  radionulcidesList?: any[] = [];
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

  transferList: any[] = [];

  // Disable Dates after today
  currentDate: any = new Date();

  // Source Variables and Attachments
  sourceFiles: File[] = [];

  manufacturerCertificates: File[] = [];
  customImportPermits: File[] = [];
  shipperImportPermits: File[] = [];
  otherDocumnets: File[] = [];
  images: File[] = [];
  sourceTagImages: File[] = [];
  charCertificates: File[] = [];

  isSourceManufacturerCertificateValid: boolean = true;
  isSourceCustomImportPermitsValid: boolean = true;
  isSourceShipperImportPermitsValid: boolean = true;
  isImagesValid: boolean = true;
  isCharCertificateValid: boolean = true;
  isSourceTagImagesValid: boolean = true;

  // Shield variables and Attachments
  isShieldDUSwitch = false;

  shieldFiles: File[] = [];

  shieldManufacturerCertificates: File[] = [];
  shieldCustomImportPermits: File[] = [];
  shieldShipperImportPermits: File[] = [];
  shieldOtherDocumnets: File[] = [];
  shieldImages: File[] = [];
  shieldCharCertificates: File[] = [];

  isShieldManufacturerCertificateValid: boolean = true;
  isShieldCustomImportPermitsValid: boolean = true;
  isShieldShipperImportPermitsValid: boolean = true;
  isShieldImagesValid: boolean = true;

  // Attachments
  selectedSourceAttachments?: any[] = [];
  selectedMaterialAttachments?: any[] = [];
  isShieldAttachmentValid: boolean = true;
  isSourceAttachmentValid: boolean = true;

  // Form Validation Variables
  submitBtnClicked: boolean = false;

  selectedRadionuclides: any[];
  filteredRadionuclides: any[] = [];

  currentEntity: any = {};
  currentFacility: any = {};

  // Form
  addNewSourceForm = this.fb.group({
    transferSource: [''],
    transactionType: [this.addTypes[0], [Validators.required]],
    transactionHeaderId: [''],
    entity: [null, Validators.required],
    facility: [null, Validators.required],
    licenseNumber: [null, Validators.required],
    permitNumber: [null, Validators.required],
    sourceType: [this.SourceTypes.sealed],
    manufacturerSerialNo: [
      null,
      [Validators.required, Validators.minLength(3), Validators.maxLength(30)],
    ],
    manufacturer: [null],
    manufacturerCountry: [null, Validators.required],
    radionuclides: this.fb.array([
      this.fb.group({
        trnRadionuclideId: [''],
        radionuclide: ['', [Validators.required]],
        initialActivity: [null, [Validators.required]],
        activityUnit: ['', [Validators.required]],
        initialActivityDate: ['', [Validators.required, DateLessThanToday]],
      }),
    ]),
    status: [null, Validators.required],
    facilitySourceID: [null, [Validators.maxLength(30)]],
    associatedEquipment: [null, Validators.required],
    sourceAttachments: this.fb.array([]),
    isShieldDU: [false],
    shieldNuclearMaterialCode: [null],
    shieldAttachments: this.fb.array([]),
    selectedMixOrNot: ['single'],
    noManufacturerCertificateFlag: [false, Validators.required],
    noCustomImportPermitFlag: [false, Validators.required],
    noShipperImportPermitFlag: [false, Validators.required],
    noImagesFlag: [false, Validators.required],
    noCharacterizationCertificateFlag: [false, Validators.required],
    noSourceTagImageFlag: [false, Validators.required],
    sourceModel: [
      '',
      [Validators.required, Validators.minLength(3), Validators.maxLength(30)],
    ],
  });

  addRadionuclideClicked: boolean = false;

  newShieldAdded: boolean = false;

  isManufacturerCertificate: boolean = true;
  isCharCertificate: boolean = false;
  isCustomImportPermit: boolean = true;
  isShipperImportPermit: boolean = true;
  isImage: boolean = true;
  isSourceTagImage = false;

  isShieldManufacturerCertificate: boolean = true;
  isShieldCustomImportPermit: boolean = true;
  isShieldShipperImportPermit: boolean = true;
  isShieldImage: boolean = true;

  displaySelectPermitPopup: boolean = false;

  constructor(
    private sharedService: SharedService,
    private fb: FormBuilder,
    private insertNewRecordService: InsertNewRecordService,
    private itemSourcesProfileService: ItemSourcesProfileService,
    private translateService: TranslateService,
    private router: Router,
    private route: ActivatedRoute,
    private nuclearMaterialServices: NuclearMaterialServices,
    private transactionService: TransactionService,
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent
  ) {
    this.route.queryParams.subscribe((params) => {
      this.updateId = params['id'];
    });
  }

  ngOnInit(): void {
    // get language
    this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
      this.lang = l;
    });

    // get current entity and facility
    if (!this.updateId) {
      this.sharedService.currentEntity.subscribe((entity) => {
        this.currentEntity = entity;
        if (!entity) {
          this.router.navigate(['/home']);
        }
        this.addNewSourceForm.controls['entity'].setValue(entity);
        this.addNuclearMaterial.controls['entityId'].setValue(entity);
      });

      this.sharedService.currentFacility.subscribe((facility) => {
        this.currentFacility = facility;
        if (!facility) {
          this.router.navigate(['/home']);
        }
        this.addNewSourceForm.controls['facility'].setValue(facility);
        this.addNuclearMaterial.controls['facilityId'].setValue(facility);
        this.getLicensesByFacilityId(facility.id);
      });

      this.sharedService.currentLicense.subscribe((license) => {
        this.addNewSourceForm.controls['licenseNumber'].setValue(license);
        if (!license) {
          this.router.navigate(['/home']);
        }
        this.getPermitsByLicenseId(license.id);
      });
    }

    // get lookups
    this.insertNewRecordService.getSourceFormsLookup().subscribe((res) => {
      res['data'].forEach((element) => {
        this.lookups[element['className']] = element['lookupSetTerms'];
      });
      this.statusLookup = this.lookups['Status'].filter((e) => {
        if (
          e.extraData1 == 's' ||
          e.extraData2 == 's' ||
          (e.extraData1 == null && e.extraData2 == null)
        )
          return e;
      });
      if (this.updateId) {
        this.insertNewRecordService
          .getSourceById(this.updateId)
          .subscribe((res) => {
            console.log('BYID:', res);
            if (res['data']) {
              this.source = res['data'];
              this.setUpdateData(res['data']);
            }
          });
      }
    });

    // get radionuclides
    this.insertNewRecordService.getRadionulicdes().subscribe((res) => {
      this.radionulcidesList = res['data'];
    });
  }

  handleCertificateChange(choice) {
    let noManufacturerCertificate =
      this.addNewSourceForm.controls['noManufacturerCertificateFlag'];
    let noCharCertificate =
      this.addNewSourceForm.controls['noCharacterizationCertificateFlag'];
    let noSourceTag = this.addNewSourceForm.controls['noSourceTagImageFlag'];
    let noCustomImport =
      this.addNewSourceForm.controls['noCustomImportPermitFlag'];
    let noShipperImportPermit =
      this.addNewSourceForm.controls['noShipperImportPermitFlag'];
    let imagesFlag = this.addNewSourceForm.controls['noImagesFlag'];

    if (choice == 1) {
      this.isManufacturerCertificate = !noManufacturerCertificate.value;
      this.isCharCertificate = !this.isManufacturerCertificate;
      this.isSourceTagImage = false;
      noCharCertificate.setValue(this.isManufacturerCertificate);
      noSourceTag.setValue(this.isManufacturerCertificate);
    } else if (choice == 2) {
      this.isCharCertificate = !noCharCertificate.value;
      this.isSourceTagImage = !this.isCharCertificate;
      this.isManufacturerCertificate = false;
      noManufacturerCertificate.setValue(true);
      noSourceTag.setValue(this.isCharCertificate);
    } else if (choice == 3) {
      this.isCustomImportPermit = !noCustomImport.value;
    } else if (choice == 4) {
      this.isShipperImportPermit = !noShipperImportPermit.value;
    } else if (choice == 5) {
      this.isImage = !imagesFlag.value;
    } else if (choice == 6) {
      this.isSourceTagImage = !noSourceTag.value;
    }
  }

  setUpdateData(data) {
    this.manufacturerCertificates = [];
    this.charCertificates = [];
    this.customImportPermits = [];
    this.shipperImportPermits = [];
    this.images = [];
    this.sourceTagImages = [];

    this.isUpdate = true;
    let radionuclides = data.trnItemSourceRadionuclides?.map((radionuclide) => {
      return {
        id: radionuclide.trnRadionuclideId,
        radionuclide: this.radionulcidesList.find(
          (x) => x.radionuclideId == radionuclide.radionulcideId
        ),
        initialActivity: radionuclide.initialActivity,
        initialActivityUnit: radionuclide.initialActivityUnit
          ? this.lookups['ActivityUnit'].find(
              (x) => x.lookupSetTermId == radionuclide.initialActivityUnit
            )
          : '',
        initialActivityDate: radionuclide.initialActivityDate
          ? formatDate(radionuclide.initialActivityDate, 'yyyy-MM-dd', 'en')
          : null,
      };
    });

    if (radionuclides.length > 1) {
      this.addNewSourceForm.controls['selectedMixOrNot'].setValue('mix');
    }
    let radionuclidesAsFormArray = this.addNewSourceForm.get(
      'radionuclides'
    ) as FormArray;
    radionuclidesAsFormArray.clear();
    radionuclides?.forEach((radionuclide) =>
      radionuclidesAsFormArray.push(
        this.fb.group({
          id: [radionuclide.id],
          radionuclide: [radionuclide.radionuclide, [Validators.required]],
          initialActivity: [
            radionuclide.initialActivity,
            [Validators.required],
          ],
          activityUnit: [
            radionuclide.initialActivityUnit,
            [Validators.required],
          ],
          initialActivityDate: [
            radionuclide.initialActivityDate,
            [Validators.required, DateLessThanToday],
          ],
        })
      )
    );

    let manufacturer = data.manufacturerCountryId
      ? this.lookups['Manufacturer'].find(
          (x) => x.lookupSetTermId == data.manufacturerId
        )
      : null;

    let manufacturerCountry = data.manufacturerCountryId
      ? this.lookups['ManufacturerCountry'].find(
          (x) => x.lookupSetTermId == data.manufacturerCountryId
        )
      : null;

    let status = data.status
      ? this.lookups['Status'].find((x) => x.lookupSetTermId == data.status)
      : null;

    let ac = data.associatedEquipment
      ? this.lookups['Associated Equipment'].find(
          (x) => x.lookupSetTermId == data.associatedEquipment
        )
      : null;

    let license = data.licenseId
      ? this.licenses.find((x) => x.id == data.licenseId)
      : null;

    let transactionType = data.transactionTypeId
      ? this.addTypes.find((x) => x.id == data.transactionTypeId)
      : null;

    if (data.transactionAttactcments?.length > 0) {
      data.transactionAttactcments?.map((attachment) => {
        let file = Base64ToPdf(
          attachment.fileBytes,
          attachment.fileOriginalName
        );
        let obj = {
          attachmentType: attachment.attachmentType.toString(),
          base64: attachment.fileBytes,
          contentType: attachment.contentType,
          fileSourceID: '',
          name: attachment.fileOriginalName,
          size: file.size,
        };
        attachment.attachmentType = attachment.attachmentType.toString();
        if (
          attachment.attachmentType == AttachmentsTypes.manufacturerCertificate
        ) {
          if (attachment.forShield) {
            this.shieldManufacturerCertificates.push(file);
            this.shieldAttachments.push(this.fb.group(obj));
          } else {
            this.manufacturerCertificates.push(file);
            this.sourceAttachments.push(this.fb.group(obj));
          }
        } else if (
          attachment.attachmentType == AttachmentsTypes.customImportPermits
        ) {
          if (attachment.forShield) {
            this.shieldCustomImportPermits.push(file);
            this.shieldAttachments.push(this.fb.group(obj));
          } else {
            this.customImportPermits.push(file);
            this.sourceAttachments.push(this.fb.group(obj));
          }
        } else if (
          attachment.attachmentType == AttachmentsTypes.charCertificates
        ) {
          if (attachment.forShield) {
            this.shieldCharCertificates.push(file);
            this.shieldAttachments.push(this.fb.group(obj));
          } else {
            this.charCertificates.push(file);
            this.sourceAttachments.push(this.fb.group(obj));
          }
        } else if (attachment.attachmentType == AttachmentsTypes.images) {
          if (attachment.forShield) {
            this.shieldImages.push(file);
            this.shieldAttachments.push(this.fb.group(obj));
          } else {
            this.images.push(file);
            this.sourceAttachments.push(this.fb.group(obj));
          }
        } else if (
          attachment.attachmentType == AttachmentsTypes.shipperImportPermits
        ) {
          if (attachment.forShield) {
            this.shieldShipperImportPermits.push(file);
            this.shieldAttachments.push(this.fb.group(obj));
          } else {
            this.shipperImportPermits.push(file);
            this.sourceAttachments.push(this.fb.group(obj));
          }
        } else if (
          attachment.attachmentType == AttachmentsTypes.otherDocumnets
        ) {
          if (attachment.forShield) {
            this.shieldOtherDocumnets.push(file);
            this.shieldAttachments.push(this.fb.group(obj));
          } else {
            this.otherDocumnets.push(file);
            this.sourceAttachments.push(this.fb.group(obj));
          }
        } else if (
          attachment.attachmentType == AttachmentsTypes.sourceTagImage
        ) {
          this.sourceTagImages.push(file);
          this.sourceAttachments.push(this.fb.group(obj));
        }
      });
    }

    this.addNewSourceForm.controls['sourceAttachments'].setValue(
      this.sourceAttachments.value
    );
    this.addNewSourceForm.controls['shieldAttachments'].setValue(
      this.shieldAttachments.value
    );

    if (data?.facilityProfile)
      this.getLicensesByFacilityId(data.facilityProfile.id, [
        data.licenseId,
        data.permitdetailsId,
      ]);

    this.addNewSourceForm.patchValue({
      entity: data?.entityProfile,
      facility: data?.facilityProfile,
      licenseNumber: license ? license : '',
      manufacturerSerialNo: data.manufacturerSerialNo,
      manufacturer: manufacturer ? manufacturer : '',
      manufacturerCountry: manufacturerCountry ? manufacturerCountry : '',
      status: status ? status : '',
      facilitySourceID: data?.facilitySerialNo,
      associatedEquipment: ac ? ac : '',
      isShieldDU: data.isShieldDU,
      sourceModel: data.sourceModel,
      noManufacturerCertificateFlag: data.noManufacturerCertificateFlag,
      noCustomImportPermitFlag: data.noCustomImportPermitFlag,
      noShipperImportPermitFlag: data.noShipperImportPermitFlag,
      noImagesFlag: data.noImagesFlag,
      noCharacterizationCertificateFlag: data.noCharacterizationCertificateFlag,
      noSourceTagImageFlag: data.noSourceTagImageFlag,
      transactionHeaderId: data.transactionHeaderId,
      // transactionType: transactionType ? transactionType : '',
    });

    this.addNuclearMaterial.patchValue({
      entityId: data?.entityProfile,
      facilityId: data?.facilityProfile,
    });

    this.isCustomImportPermit = !data.noCustomImportPermitFlag;
    this.isShipperImportPermit = !data.noShipperImportPermitFlag;
    this.isImage = !data.noImagesFlag;
    this.isSourceTagImage = !data.noSourceTagImageFlag;
    this.isCharCertificate = !data.noCharacterizationCertificateFlag;
    this.isManufacturerCertificate = !data.noManufacturerCertificateFlag;

    if (this.isManufacturerCertificate) {
      this.isSourceTagImage = false;
      this.isCharCertificate = false;
    }
    if (this.isCharCertificate) {
      this.isSourceTagImage = false;
      this.isManufacturerCertificate = false;
    }
    if (this.isTransfer) {
      this.addNewSourceForm.controls['facilitySourceID'].setValue(null);
      this.addNewSourceForm.controls['status'].setValue(null);
    }
  }

  getSourcesByPermitId() {
    let transactionType =
      this.addNewSourceForm.controls['transactionType'].value;
    if (transactionType.id == 2) {
      this.isTransfer = true;
      let permit = this.addNewSourceForm.controls['permitNumber'].value;
      if (permit) {
        this.transactionService
          .GetSourcesByPermit(permit['id'], SourceTypes.sealed)
          .subscribe((res) => {
            console.log(res);
            this.transferList = res['data'];
          });
      } else {
        this.displaySelectPermitPopup = true;
      }
    }
  }

  // get selected source form permit
  getSelectedSourceFromPermit() {
    let source = this.addNewSourceForm.controls['transferSource'].value;
    console.log(source);
    this.insertNewRecordService.getSourceById(source['id']).subscribe((res) => {
      if (res['data']) {
        console.log('ById', res['data']);
        this.source = res['data'];
        this.setUpdateData(res['data']);
      }
    });
  }
  // Radionuclides FormArray Methods
  get radionuclides() {
    return this.addNewSourceForm.get('radionuclides') as FormArray;
  }

  addRadionuclides() {
    let radionuclides = this.addNewSourceForm.get('radionuclides') as FormArray;

    radionuclides.push(
      this.fb.group({
        radionuclide: ['', [Validators.required]],
        initialActivity: [null, [Validators.required]],
        activityUnit: ['', [Validators.required]],
        initialActivityDate: ['', [Validators.required, DateLessThanToday]],
      })
    );
  }

  removeRadionuclides(i: number) {
    this.radionuclides.removeAt(i);
  }

  getRadionulcideDisplayNameById(id: string) {
    return this.radionulcidesList.find((x) => x.id === id).displayName;
  }

  // Source Attachments FormArray Methods
  get sourceAttachments() {
    return this.addNewSourceForm.get('sourceAttachments') as FormArray;
  }

  removeSourceAttachment(i: number) {
    this.sourceAttachments.removeAt(i);
  }

  addSourceAttachments(file, attachmentType) {
    let StrBase64;

    PdfToBase64(file)
      .then((res) => (StrBase64 = res))
      .then((res) => {
        return {
          fileSourceID: '',
          name: file.name,
          size: file.size,
          base64: StrBase64.toString(),
          contentType: file.type,
          attachmentType: attachmentType,
        };
      })
      .then((res) => {
        this.sourceAttachments.push(this.fb.group(res));
      });
  }

  // Shield Attachments FormArray Methods
  get shieldAttachments() {
    return this.addNuclearMaterial.get('nuclearMaterialFiles') as FormArray;
  }

  removeShieldAttachment(i: number) {
    this.shieldAttachments.removeAt(i);
  }

  addShieldAttachments(file, attachmentType) {
    let StrBase64;

    PdfToBase64(file)
      .then((res) => (StrBase64 = res))
      .then((res) => {
        return {
          fileSourceID: '',
          name: file.name,
          size: file.size,
          base64: StrBase64.toString(),
          contentType: file.type,
          attachmentType: attachmentType,
          forShield: true,
        };
      })
      .then((res) => {
        this.shieldAttachments.push(this.fb.group(res));
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
          this.getPermitsByLicenseId(license.id, updateIds);
          this.getShields();
        }
      }
    });
  }
  // Get Permits by License
  getPermitsByLicenseId(id: string, updateIds?: number[]) {
    this.insertNewRecordService.getPermits(id).subscribe((res) => {
      this.permits = res['data'];
      if (this.isUpdate) {
        let permit = this.permits.find(
          (x) => x.permitDetailsId == updateIds[1]
        );
        this.addNewSourceForm.controls['permitNumber'].setValue(permit);
      }
    });
  }

  // Handle Shield Material Change
  handleisShieldDUChange(event) {
    this.isShieldDUSwitch = event.checked;
    if (this.isShieldDUSwitch) {
      this.addNewSourceForm.controls['shieldNuclearMaterialCode'].enable();
      this.getShields();
    } else {
      this.addNewSourceForm.controls['shieldNuclearMaterialCode'].disable();
      this.addNewSourceForm.controls['weightOfNuclearMaterial'].disable();
      this.addNewSourceForm.controls['weightOfNuclearMaterialUnit'].disable();
      this.addNewSourceForm.controls['enrichmentOfUranium'].disable();
      this.addNewSourceForm.controls['shieldChemicalForm'].disable();
      this.addNewSourceForm.controls['shieldAttachments'].disable();
    }
  }

  // Attachments Methods
  onSelect(event, field) {
    this.sourceFiles = [];
    this.sourceFiles.push(...event.addedFiles);
    for (let i = 0; i < this.sourceFiles.length; i++) {
      this.addSourceAttachments(<File>this.sourceFiles[i], field);

      if (field == AttachmentsTypes.manufacturerCertificate)
        this.manufacturerCertificates.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.customImportPermits)
        this.customImportPermits.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.images)
        this.images.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.shipperImportPermits)
        this.shipperImportPermits.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.charCertificates)
        this.charCertificates.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.sourceTagImage)
        this.sourceTagImages.push(this.sourceFiles[i]);
      else this.otherDocumnets.push(this.sourceFiles[i]);
    }
  }

  onRemove(event, field) {
    this.sourceFiles = [];

    if (field == AttachmentsTypes.manufacturerCertificate) {
      this.manufacturerCertificates.splice(
        this.manufacturerCertificates.indexOf(event),
        1
      );
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    } else if (field == AttachmentsTypes.customImportPermits) {
      this.customImportPermits.splice(
        this.customImportPermits.indexOf(event),
        1
      );
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    } else if (field == AttachmentsTypes.images) {
      this.images.splice(this.images.indexOf(event), 1);
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    } else if (field == AttachmentsTypes.shipperImportPermits) {
      this.shipperImportPermits.splice(
        this.shipperImportPermits.indexOf(event),
        1
      );
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    } else if (field == AttachmentsTypes.charCertificates) {
      this.charCertificates.splice(this.charCertificates.indexOf(event), 1);
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    } else if (field == AttachmentsTypes.sourceTagImage) {
      this.sourceTagImages.splice(this.sourceTagImages.indexOf(event), 1);
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    } else {
      this.otherDocumnets.splice(this.otherDocumnets.indexOf(event), 1);
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    }

    this.addNewSourceForm.controls['sourceAttachments'].clear();

    for (let i = 0; i < this.manufacturerCertificates.length; i++) {
      this.addSourceAttachments(
        <File>this.manufacturerCertificates[i],
        AttachmentsTypes.manufacturerCertificate
      );
    }
    for (let i = 0; i < this.customImportPermits.length; i++) {
      this.addSourceAttachments(
        <File>this.customImportPermits[i],
        AttachmentsTypes.customImportPermits
      );
    }
    for (let i = 0; i < this.images.length; i++) {
      this.addSourceAttachments(<File>this.images[i], AttachmentsTypes.images);
    }
    for (let i = 0; i < this.shipperImportPermits.length; i++) {
      this.addSourceAttachments(
        <File>this.shipperImportPermits[i],
        AttachmentsTypes.shipperImportPermits
      );
    }
    for (let i = 0; i < this.otherDocumnets.length; i++) {
      this.addSourceAttachments(
        <File>this.otherDocumnets[i],
        AttachmentsTypes.otherDocumnets
      );
    }

    for (let i = 0; i < this.sourceTagImages.length; i++) {
      this.addSourceAttachments(
        <File>this.sourceTagImages[i],
        AttachmentsTypes.sourceTagImage
      );
    }

    for (let i = 0; i < this.charCertificates.length; i++) {
      this.addSourceAttachments(
        <File>this.charCertificates[i],
        AttachmentsTypes.charCertificates
      );
    }
  }

  onSelectShieldFile(event, field) {
    this.shieldFiles = [];
    this.shieldFiles.push(...event.addedFiles);
    for (let i = 0; i < this.shieldFiles.length; i++) {
      this.addShieldAttachments(<File>this.shieldFiles[i], field);

      if (field == AttachmentsTypes.manufacturerCertificate)
        this.shieldManufacturerCertificates.push(this.shieldFiles[i]);
      else if (field == AttachmentsTypes.customImportPermits)
        this.shieldCustomImportPermits.push(this.shieldFiles[i]);
      else if (field == AttachmentsTypes.images)
        this.shieldImages.push(this.shieldFiles[i]);
      else if (field == AttachmentsTypes.shipperImportPermits)
        this.shieldShipperImportPermits.push(this.shieldFiles[i]);
      else this.shieldOtherDocumnets.push(this.shieldFiles[i]);
    }
  }

  onRemoveShieldFile(event, field) {
    this.shieldFiles = [];

    if (field == AttachmentsTypes.manufacturerCertificate) {
      this.shieldManufacturerCertificates.splice(
        this.shieldManufacturerCertificates.indexOf(event),
        1
      );
      this.shieldFiles.push(...this.shieldManufacturerCertificates);
      this.shieldFiles.push(...this.shieldCustomImportPermits);
      this.shieldFiles.push(...this.shieldImages);
      this.shieldFiles.push(...this.shieldShipperImportPermits);
      this.shieldFiles.push(...this.shieldOtherDocumnets);
    } else if (field == AttachmentsTypes.customImportPermits) {
      this.shieldCustomImportPermits.splice(
        this.shieldCustomImportPermits.indexOf(event),
        1
      );
      this.shieldFiles.push(...this.shieldManufacturerCertificates);
      this.shieldFiles.push(...this.shieldCustomImportPermits);
      this.shieldFiles.push(...this.shieldImages);
      this.shieldFiles.push(...this.shieldShipperImportPermits);
      this.shieldFiles.push(...this.shieldOtherDocumnets);
    } else if (field == AttachmentsTypes.images) {
      this.shieldImages.splice(this.shieldImages.indexOf(event), 1);
      this.shieldFiles.push(...this.shieldManufacturerCertificates);
      this.shieldFiles.push(...this.shieldCustomImportPermits);
      this.shieldFiles.push(...this.shieldImages);
      this.shieldFiles.push(...this.shieldShipperImportPermits);
      this.shieldFiles.push(...this.shieldOtherDocumnets);
    } else if (field == AttachmentsTypes.shipperImportPermits) {
      this.shieldShipperImportPermits.splice(
        this.shieldShipperImportPermits.indexOf(event),
        1
      );
      this.shieldFiles.push(...this.shieldManufacturerCertificates);
      this.shieldFiles.push(...this.shieldCustomImportPermits);
      this.shieldFiles.push(...this.shieldImages);
      this.shieldFiles.push(...this.shieldShipperImportPermits);
      this.shieldFiles.push(...this.shieldOtherDocumnets);
    } else {
      this.shieldOtherDocumnets.splice(
        this.shieldOtherDocumnets.indexOf(event),
        1
      );
      this.shieldFiles.push(...this.shieldManufacturerCertificates);
      this.shieldFiles.push(...this.shieldCustomImportPermits);
      this.shieldFiles.push(...this.shieldImages);
      this.shieldFiles.push(...this.shieldShipperImportPermits);
      this.shieldFiles.push(...this.shieldOtherDocumnets);
    }

    this.addNewSourceForm.controls['shieldAttachments'].clear();

    for (let i = 0; i < this.shieldManufacturerCertificates.length; i++) {
      this.addShieldAttachments(
        <File>this.shieldManufacturerCertificates[i],
        AttachmentsTypes.manufacturerCertificate
      );
    }
    for (let i = 0; i < this.shieldCustomImportPermits.length; i++) {
      this.addShieldAttachments(
        <File>this.shieldCustomImportPermits[i],
        AttachmentsTypes.customImportPermits
      );
    }
    for (let i = 0; i < this.shieldImages.length; i++) {
      this.addShieldAttachments(
        <File>this.shieldImages[i],
        AttachmentsTypes.customExportPermits
      );
    }
    for (let i = 0; i < this.shieldShipperImportPermits.length; i++) {
      this.addShieldAttachments(
        <File>this.shieldShipperImportPermits[i],
        AttachmentsTypes.shipperImportPermits
      );
    }
    for (let i = 0; i < this.shieldOtherDocumnets.length; i++) {
      this.addShieldAttachments(
        <File>this.shieldOtherDocumnets[i],
        AttachmentsTypes.otherDocumnets
      );
    }
  }

  validateAttachments(): boolean {
    let list = this.addNewSourceForm.controls['sourceAttachments'].value;
    if (this.isManufacturerCertificate)
      this.isSourceManufacturerCertificateValid =
        this.manufacturerCertificates.length == 0 ? false : true;
    else this.isSourceManufacturerCertificateValid = true;

    if (this.isCharCertificate)
      this.isCharCertificateValid =
        this.charCertificates.length == 0 ? false : true;
    else this.isCharCertificateValid = true;

    if (this.isCustomImportPermit) {
      this.isSourceCustomImportPermitsValid =
        this.customImportPermits.length == 0 ? false : true;
    } else this.isSourceCustomImportPermitsValid = true;

    if (this.isShipperImportPermit) {
      this.isSourceShipperImportPermitsValid =
        this.shipperImportPermits.length == 0 ? false : true;
    } else this.isSourceShipperImportPermitsValid = true;

    if (this.isImage) {
      this.isImagesValid = this.images.length == 0 ? false : true;
    } else this.isImagesValid = true;

    if (this.isSourceTagImage && !this.charCertificates) {
      this.isSourceTagImagesValid =
        this.sourceTagImages.length == 0 ? false : true;
    } else this.isSourceTagImagesValid = true;
    if (
      this.isSourceManufacturerCertificateValid &&
      this.isCharCertificateValid &&
      this.isSourceCustomImportPermitsValid &&
      this.isSourceShipperImportPermitsValid &&
      this.isImagesValid &&
      this.isSourceTagImagesValid
    ) {
      this.addNewSourceForm.controls['sourceAttachments'].setErrors(null);
      this.isSourceAttachmentValid = true;
      return true;
    }

    return false;
  }

  mapInputWithModel(body, sourceStatus): AddNewSourceModel | null {
    try {
      let radionuclides = this.addNewSourceForm.controls['radionuclides'].value;

      let radionuclidesList = radionuclides.map((item) => {
        return {
          radionuclideTrnId: item['id'] ? item['id'] : null,
          radionuclide: item.radionuclide ? item.radionuclide['id'] : null,
          initialActivity: item.initialActivity,
          initialActivityUnit: item.activityUnit
            ? item.activityUnit['id']
            : null,
          initialActivityDate: item.initialActivityDate
            ? item.initialActivityDate
            : null,
        };
      });

      let sourceTypeEnum = this.addNewSourceForm.controls['sourceType'].value;
      let sourceType = this.lookups['SourceType'].find((x) => x.value == 1).id;

      let obj: AddNewSourceModel = {
        id: this.updateId ? this.updateId : null,
        transactionType: body.transactionType?.id,
        entity: body.entity?.id,
        facility: body.facility?.id,
        licenseNumber: body.licenseNumber?.id,
        permitNumber: body.permitNumber?.id,
        sourceType: sourceType,
        manufacturerSerialNo: body.manufacturerSerialNo,
        manufacturer: body.manufacturer?.id,
        manufacturerCountry: body.manufacturerCountry?.id,
        status: body.status?.id,
        facilitySourceID: body.facilitySourceID,
        associatedEquipment: body.associatedEquipment?.id,
        sourceAttachments: body.sourceAttachments,
        radionuclides: radionuclidesList,
        shieldNuclearMaterialCode: body.shieldNuclearMaterialCode?.sourceId,
        initialMass: 0,
        initialMassUnit: null,
        shieldAttachments: [],
        physicalForm: '',
        sourceActivity: null,
        sourceActivityUnit: '',
        sourceStatus: sourceStatus,
        quantity: 0,
        isShieldDU: body.isShieldDU,
        sourceModel: body.sourceModel,
        noManufacturerCertificateFlag: body.noManufacturerCertificateFlag,
        noCustomImportPermitFlag: body.noCustomImportPermitFlag,
        noShipperImportPermitFlag: body.noShipperImportPermitFlag,
        noSourceTagImageFlag: body.noSourceTagImageFlag,
        noImagesFlag: body.noImagesFlag,
        noCharacterizationCertificateFlag:
          body.noCharacterizationCertificateFlag,
        transactionHeaderId: body.transactionHeaderId
          ? body.transactionHeaderId
          : null,
      };

      if (this.isTransfer) {
        obj['id'] = body.transferSource.id;
      }
      return obj;
    } catch (error) {
      return null;
    }
  }

  submitNewSource() {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth',
    });
    this.submitBtnClicked = true;
    let attachmentsStatus = this.validateAttachments();
    let body = this.addNewSourceForm.value;
    let load = this.mapInputWithModel(body, SourceStatus.submit);
    if (load) {
      if (attachmentsStatus && this.addNewSourceForm.valid) {
        this.app.confirmationService.confirm({
          message: this.translateService.instant('legalStatmentMessage'),
          header: this.translateService.instant('Confirmation'),
          icon: 'pi pi-exclamation-triangle',
          accept: () => {
            this.itemSourcesProfileService.saveItemSource(load).subscribe(
              (res) => {
                if (res['succeeded']) {
                  this.app.messageService.add({
                    severity: 'success',
                    key: 'PlanValidation',
                    summary: this.translateService.instant(
                      'Submitted Successfully'
                    ),
                    detail: this.translateService.instant(
                      'Submitted Successfully'
                    ),
                    life: 6000,
                  });
                  this.router.navigate(['']);
                } else if (res['message'] == 'exist') {
                  this.app.messageService.add({
                    severity: 'error',
                    key: 'PlanValidation',
                    summary: 'Error',
                    detail: this.translateService.instant('msnAleardyExist'),
                    life: 6000,
                  });
                } else {
                  this.app.messageService.add({
                    severity: 'error',
                    key: 'PlanValidation',
                    summary: 'Error',
                    detail: 'Something Went Wrong Please Contact the admin',
                    life: 6000,
                  });
                }
              },
              (err) => {
                this.app.messageService.add({
                  severity: 'error',
                  key: 'PlanValidation',
                  summary: 'Error',
                  detail: this.translateService.instant('validationMessage'),
                  life: 6000,
                });
              }
            );
          },
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
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth',
    });
    let body = this.addNewSourceForm.value;

    let load = this.mapInputWithModel(body, SourceStatus.draft);
    console.log('load', load);

    if (load) {
      if (this.updateId) load.id = this.updateId;
      this.itemSourcesProfileService.saveItemSource(load).subscribe((res) => {
        if (res['succeeded']) {
          this.app.messageService.add({
            severity: 'success',
            key: 'PlanValidation',
            summary: this.translateService.instant('Saved As Draft'),
            detail: this.translateService.instant('Saved As Draft'),
            life: 6000,
          });
          this.router.navigate(['']);
        } else if (res['message'] == 'exist') {
          this.app.messageService.add({
            severity: 'error',
            key: 'PlanValidation',
            summary: 'Error',
            detail: this.translateService.instant('msnAleardyExist'),
            life: 6000,
          });
        }
      });
    }
  }

  submitNuclearClicked = false;
  shieldPremits: any[] = [];

  addNuclearMaterial = this.fb.group({
    manufacturerSerialNo: [
      '',
      [Validators.required, Validators.minLength(3), Validators.maxLength(12)],
    ],
    initialMass: [null, Validators.required],
    isShield: [true, Validators.required],
    initialMassUnit: [null, Validators.required],
    entityId: [null, Validators.required],
    facilityId: [null, Validators.required],
    licenseId: [null, Validators.required],
    permitdetailsId: [null, Validators.required],
    nuclearMaterialFiles: this.fb.array([]),
    sourceModel: [
      '',
      [Validators.required, Validators.minLength(3), Validators.maxLength(12)],
    ],
    noManufacturerCertificateFlag: [false],
    noCustomImportPermitFlag: [false],
    noShipperImportPermitFlag: [false],
    noImagesFlag: [false],
  });

  getShieldPermitsByLicenseId(id: string) {
    this.insertNewRecordService.getPermits(id).subscribe((res) => {
      this.shieldPremits = res['data'];
    });
  }

  handleShieldCertificateChange(choice) {
    if (choice == 1) {
      this.isShieldManufacturerCertificate =
        !this.isShieldManufacturerCertificate;
    } else if (choice == 2) {
      this.isShieldCustomImportPermit = !this.isShieldCustomImportPermit;
    } else if (choice == 3) {
      this.isShieldShipperImportPermit = !this.isShieldShipperImportPermit;
    } else if (choice == 4) {
      this.isShieldImage = !this.isShieldImage;
    }
  }

  validateShieldAttachments() {
    if (this.isShieldManufacturerCertificate)
      this.isShieldManufacturerCertificateValid =
        this.shieldManufacturerCertificates.length == 0 ? false : true;
    else this.isShieldManufacturerCertificateValid = true;

    if (this.isShieldCustomImportPermit) {
      this.isShieldCustomImportPermitsValid =
        this.shieldCustomImportPermits.length == 0 ? false : true;
    } else this.isShieldCustomImportPermitsValid = true;

    if (this.isShieldShipperImportPermit) {
      this.isShieldShipperImportPermitsValid =
        this.shieldShipperImportPermits.length == 0 ? false : true;
    } else this.isShieldShipperImportPermitsValid = true;

    if (this.isShieldImage) {
      this.isShieldImagesValid = this.shieldImages.length == 0 ? false : true;
    } else this.isShieldImagesValid = true;

    if (
      this.isShieldManufacturerCertificateValid &&
      this.isShieldCustomImportPermitsValid &&
      this.isShieldShipperImportPermitsValid &&
      this.isShieldImagesValid
    ) {
      this.addNuclearMaterial.controls['nuclearMaterialFiles'].setErrors(null);
      this.isShieldAttachmentValid = true;
    } else {
      this.isShieldAttachmentValid = false;
    }
  }

  getShields() {
    let license = this.addNewSourceForm.controls['licenseNumber'].value;
    console.log('this.source', this.source);

    if (license) {
      this.nuclearMaterialServices
        .getNuclearMaterialsAsLookup(license['id'])
        .subscribe((res) => {
          this.shieldCodeList = res['data'];
          console.log('SHIELDS:', this.shieldCodeList);
          let shieldCode = this.source.shieldNuclearMaterialCode
            ? this.shieldCodeList.find(
                (x) => x.sourceId == this.source.shieldNuclearMaterialCode
              )
            : null;

          console.log('SHIELD CODE:', shieldCode);
          this.addNewSourceForm.controls['shieldNuclearMaterialCode'].setValue(
            shieldCode
          );
        });
    }
  }

  onAddShieldModal() {
    let license = this.addNewSourceForm.controls['licenseNumber'].value;

    if (license) {
      this.getShieldPermitsByLicenseId(license['id']);
      this.addNuclearMaterial.controls['licenseId'].setValue(license);
    }
  }

  submitNuclearShield() {
    this.submitNuclearClicked = true;
    this.newShieldAdded = true;
    this.validateShieldAttachments();
    let body = this.addNuclearMaterial.value;
    let load = {
      manufacturerSerialNo: body.manufacturerSerialNo,
      initialMass: body.initialMass,
      isShield: body.isShield,
      initialMassUnit: body.initialMassUnit ? body.initialMassUnit['id'] : '',
      entityId: body.entityId ? body.entityId['id'] : '',
      facilityId: body.facilityId ? body.facilityId['id'] : '',
      licenseId: body.licenseId ? body.licenseId['id'] : '',
      permitdetailsId: body.permitdetailsId ? body.permitdetailsId['id'] : '',
      nuclearMaterialFiles: body.nuclearMaterialFiles,
      sourceModel: body.sourceModel,
    };

    console.log('LOAD:', load);
    if (this.addNuclearMaterial.valid && this.isShieldAttachmentValid) {
      console.log(this.addNuclearMaterial.value);
      this.nuclearMaterialServices.addNuclearShield(load).subscribe((res) => {
        if (res['succeeded']) {
          console.log(res);
          this.shieldCodeList.push(res['data']);
          this.addNewSourceForm.controls[
            'shieldNuclearMaterialCode'
          ].patchValue(res['data']);

          this.app.messageService.add({
            severity: 'success',
            key: 'PlanValidation',
            summary: this.translateService.instant('Submitted Successfully'),
            detail: this.translateService.instant('Submitted Successfully'),
            life: 6000,
          });
        }
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
}
