import { Component, OnInit } from '@angular/core';
import { elementAt } from 'rxjs';
import { AppSettings } from 'src/app/AppSettings';
import { PdfToBase64 } from 'src/app/helper/fileValidation';
import { AttachmentModel } from 'src/app/models/AttachmentModel/AttachmentModel';
import * as $ from 'jquery';
@Component({
  selector: 'app-pdf-file',
  templateUrl: './pdf-file.component.html',
  styleUrls: ['./pdf-file.component.css']
})
export class PdfFileComponent implements OnInit {
  attachments = [];
  constructor() {
  }

  ngOnInit(): void {
  }

  onChange($event:Event) {
      const files = ($event.target as HTMLInputElement).files;
      console.log(files.length)
      let ImageBase64;

      for(let i = 0; i < files.length; i++)
      {
        if(files[i].size>AppSettings.maxFileSize){
        alert("this is a large file size")
        $("#pdfUpload").val('');
        break;
        }else{
          PdfToBase64(files[i]).then(res => 
            ImageBase64 = res
          ).then((res) => {
            let attachment: AttachmentModel = {
                      Id: "",
                      CreatedBy: "",
                      ModifiedBy: "",
                      CreatedOn:null,
                      ModifiedOn: null,
                      FileSourceID: "",
                      FileName: files[i].name,
                      FileSize: files[i].size,
                      FileStringBase62: "",
                      ContentType: files[i].type
                    };
            console.log(ImageBase64)
            console.log("hi")

            attachment.FileStringBase62 = res.toString();
            this.attachments.push(attachment)
          },
          ); 
        
          
        }
       
      }

     // console.log("files", this.attachments)
}
}
