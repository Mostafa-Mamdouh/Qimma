import { Component, forwardRef, OnInit, Inject } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { faPlus, faXmark } from '@fortawesome/free-solid-svg-icons';
import { Subscription } from 'rxjs';
import { SourceStatus, SourceTypes } from 'src/app/Enumerations/sourceTypes';
import { DateLessThanToday } from 'src/app/helper/validations/DateLessThanToday';
import { AddNewSourceModel } from 'src/app/models/addNewSourceModel/addNewSourceModel';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { InsertNewRecordService } from './../../../services/Insert-New-Recored/insert-new-record.service';
import { AttachmentsTypes } from 'src/app/Enumerations/attachments';
import { currentActivty } from 'src/app/helper/calculations';
import { Base64ToPdf, PdfToBase64 } from 'src/app/helper/fileValidation';
import { AppComponent } from 'src/app/app.component';
import { TranslateService } from '@ngx-translate/core';
import { TransactionTypes } from 'src/app/Enumerations/TransactionTypes';
import { ActivatedRoute, Router } from '@angular/router';
import { ItemSourcesProfileService } from 'src/app/services/ItemSourcesProfile/item-sources-profile.service';
import { formatDate } from '@angular/common';
import { TransactionService } from 'src/app/services/TransactionService/TransactionService.service';
declare var $: any;

@Component({
  selector: 'app-add-new-variableunsealed-source',
  templateUrl: './add-new-variableunsealed-source.component.html',
  styleUrls: ['./add-new-variableunsealed-source.component.css'],
})
export class AddNewVariableunsealedSourceComponent implements OnInit {
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

  // Disable Dates after today
  currentDate: any = new Date();

  // Source Variables and Attachments
  sourceFiles: File[] = [];

  manufacturerCertificates: File[] = [];
  customImportPermits: File[] = [];
  shipperImportPermits: File[] = [];
  otherDocumnets: File[] = [];
  images: File[] = [];
  charCertificates: File[] = [];
  sourceTagImages: File[] = [];

  isSourceManufacturerCertificateValid: boolean = true;
  isSourceCustomImportPermitsValid: boolean = true;
  isSourceShipperImportPermitsValid: boolean = true;
  isImagesValid: boolean = true;
  isCharCertificateValid: boolean = true;

  // Attachments
  selectedSourceAttachments?: any[] = [];
  selectedMaterialAttachments?: any[] = [];
  isSourceAttachmentValid: boolean = true;
  isSourceTagImagesValid: boolean = true;

  currentEntity: any = {};
  currentFacility: any = {};

  transferList: any[] = [];

  // Form Validation Variables
  submitBtnClicked: boolean = false;

  // Form
  addNewSourceForm = this.fb.group({
    transferSource: [''],
    transactionType: [this.addTypes[0], [Validators.required]],
    transactionHeaderId: [''],
    facility: [null, Validators.required],
    licenseNumber: [null, Validators.required],
    permitNumber: [null, Validators.required],
    sourceType: [this.SourceTypes.variableUnsealed],
    manufacturerSerialNo: [
      null,
      [Validators.required, Validators.minLength(3), Validators.maxLength(30)],
    ],
    manufacturer: [null],
    manufacturerCountry: [null, Validators.required],
    radionuclides: this.fb.array([
      this.fb.group({
        radionuclide: ['', Validators.required],
        initialActivity: 0,
        initialActivityUnit: null,
        initialActivityDate: null,
      }),
    ]),
    associatedEquipment: [null, Validators.required],
    sourceAttachments: this.fb.array([]),
    physicalForm: [null, Validators.required],
    quantity: [null, Validators.required],

    sourceActivity: [null, Validators.required],
    sourceActivityUnit: [null, Validators.required],
    entity: [null, Validators.required],

    selectedMixOrNot: ['single'],
    noManufacturerCertificateFlag: [false, Validators.required],
    noCustomImportPermitFlag: [false, Validators.required],
    noShipperImportPermitFlag: [false, Validators.required],
    noImagesFlag: [false, Validators.required],
    noCharacterizationCertificateFlag: [false, Validators.required],
    noSourceTagImageFlag: [false, Validators.required],
  });

  addRadionuclideClicked: boolean = false;
  isManufacturerCertificate: boolean = true;
  isCharCertificate: boolean = false;
  isCustomImportPermit: boolean = true;
  isShipperImportPermit: boolean = true;
  isImage: boolean = true;
  isSourceTagImage = false;

  displaySelectPermitPopup: boolean = false;

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private sharedService: SharedService,
    private fb: FormBuilder,
    private itemSourcesProfileService: ItemSourcesProfileService,
    private insertNewRecordService: InsertNewRecordService,
    private translateService: TranslateService,
    private router: Router,
    private transactionService: TransactionService,
    private route: ActivatedRoute
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
        console.log(entity);
        if (!entity) {
          this.router.navigate(['/home']);
        }
        this.addNewSourceForm.controls['entity'].setValue(entity);
      });

      this.sharedService.currentFacility.subscribe((facility) => {
        console.log(facility);
        if (!facility) {
          this.router.navigate(['/home']);
        }
        this.addNewSourceForm.controls['facility'].setValue(facility);
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
    this.insertNewRecordService.getAllLookups().subscribe((res) => {
      res['data'].forEach((element) => {
        this.lookups[element['className']] = element['lookupSetTerms'];
      });

      this.statusLookup = this.lookups['Status'].filter((e) => {
        if (
          e.extraData1 == 'v' ||
          e.extraData2 == 'v' ||
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

  setUpdateData(data) {
    this.isUpdate = true;
    let radionuclides = data.trnItemSourceRadionuclides?.map((radionuclide) => {
      return {
        radionuclide: this.radionulcidesList.find(
          (x) => x.radionuclideId == radionuclide.radionulcideId
        ),
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
          radionuclide: [radionuclide.radionuclide, [Validators.required]],
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

    let pf = data.physicalForm
      ? this.lookups['PhysicalForm'].find(
          (x) => x.lookupSetTermId == data.physicalForm
        )
      : null;

    let activityUnit = data.physicalForm
      ? this.lookups['ActivityUnit'].find(
          (x) => x.lookupSetTermId == data.sourceActivityUnit
        )
      : null;

    let license = data.licenseId
      ? this.licenses.find((x) => x.id == data.licenseId)
      : null;

    if (data.transactionAttactcments?.length > 0) {
      data.transactionAttactcments?.map((attachment) => {
        console.log('attachment', attachment);
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
        this.sourceAttachments.push(this.fb.group(obj));

        if (
          attachment.attachmentType == AttachmentsTypes.manufacturerCertificate
        ) {
          this.manufacturerCertificates.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.customImportPermits
        ) {
          this.customImportPermits.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.shipperImportPermits
        ) {
          this.shipperImportPermits.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.otherDocumnets
        ) {
          this.otherDocumnets.push(file);
        } else if (attachment.attachmentType == AttachmentsTypes.images) {
          this.images.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.charCertificates
        ) {
          this.charCertificates.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.sourceTagImage
        ) {
          this.sourceTagImages.push(file);
        }
      });
    }
    this.addNewSourceForm.controls['sourceAttachments'].setValue(
      this.sourceAttachments.value
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
      associatedEquipment: ac ? ac : '',
      physicalForm: pf ? pf : '',
      quantity: data.quantity,
      sourceActivity: data.sourceActivity,
      sourceActivityUnit: activityUnit ? activityUnit : '',
      noManufacturerCertificateFlag: data.noManufacturerCertificateFlag,
      noCustomImportPermitFlag: data.noCustomImportPermitFlag,
      noShipperImportPermitFlag: data.noShipperImportPermitFlag,
      noImagesFlag: data.noImagesFlag,
      noCharacterizationCertificateFlag: data.noCharacterizationCertificateFlag,
      noSourceTagImageFlag: data.noSourceTagImageFlag,
      transactionHeaderId: data.transactionHeaderId,
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
          .GetSourcesByPermit(permit['id'], SourceTypes.variableUnsealed)
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
  // Radionuclides FormArray Methods
  get radionuclides() {
    return this.addNewSourceForm.get('radionuclides') as FormArray;
  }

  addRadionuclides(count) {
    let radionuclides = this.addNewSourceForm.get('radionuclides') as FormArray;
    let len = radionuclides.length;

    if (parseInt(count) > 0) {
      if (len > parseInt(count)) {
        for (let i = 0; i < len - parseInt(count); i++) {
          radionuclides.removeAt(radionuclides.length - 1);
        }
      }

      for (let i = radionuclides.length; i <= parseInt(count) - 1; i++) {
        radionuclides.push(
          this.fb.group({
            radionuclide: [null, [Validators.required]],
            initialActivityDate: ['', [Validators.required, DateLessThanToday]],
          })
        );
      }
    }
    this.addRadionuclideClicked = true;
  }

  removeRadionuclides(i: number) {
    this.radionuclides.removeAt(i);
  }

  // Source Attachments FormArray Methods
  get sourceAttachments() {
    return this.addNewSourceForm.get('sourceAttachments') as FormArray;
  }

  addSourceAttachments(file, attachmentType) {
    let StrBase64;

    PdfToBase64(file)
      .then((res) => (StrBase64 = res))
      .then((res) => {
        return {
          fileSourceID: null,
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

  removeSourceAttachment(i: number) {
    this.sourceAttachments.removeAt(i);
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

    if (this.isSourceTagImage) {
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
        };
      });

      let sourceTypeEnum = this.addNewSourceForm.controls['sourceType'].value;
      let sourceType = this.lookups['SourceType'].find((x) => x.value == 3).id;

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
        shieldNuclearMaterialCode: '',
        initialMass: 0,
        initialMassUnit: null,
        shieldAttachments: [],
        physicalForm: body.physicalForm?.id,
        sourceActivity: body.sourceActivity,
        sourceActivityUnit: body.sourceActivityUnit?.id,
        sourceStatus: sourceStatus,
        quantity: body.quantity,
        isShieldDU: false,
        sourceModel: body.sourceModel,
        noManufacturerCertificateFlag: body.noManufacturerCertificateFlag,
        noCustomImportPermitFlag: body.noCustomImportPermitFlag,
        noShipperImportPermitFlag: body.noShipperImportPermitFlag,
        noImagesFlag: body.noImagesFlag,
        noSourceTagImageFlag: body.NoSourceTagImageFlag,
        noCharacterizationCertificateFlag:
          body.noCharacterizationCertificateFlag,
        transactionHeaderId: body.transactionHeaderId
          ? body.transactionHeaderId
          : null,
      };
      return obj;
    } catch (error) {
      console.log(error);
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
    if (this.updateId) load.id = this.updateId;
    console.log(load);
    if (load) {
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
}
