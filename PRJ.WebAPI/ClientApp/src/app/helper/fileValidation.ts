import { AttachmentModel } from 'src/app/models/AttachmentModel/AttachmentModel';

export const PdfToBase64 = (file) => {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
  });
};

export const Base64ToPdf = (base64: string, fileName: string) => {
  const arr = base64.split(',');
  const mime = arr[0].match(/:(.*?);/)[1];
  const bstr = atob(arr[1]);
  let n = bstr.length;
  const u8arr = new Uint8Array(n);
  while (n--) {
    u8arr[n] = bstr.charCodeAt(n);
  }
  return new File([u8arr], fileName, { type: mime });
};

/*
var holder:String="";
for (let i = 0; i < file.length; i++) {
    var reader = new FileReader();
    reader.onloadend = () => {
        // Use a regex to remove data url part
       var base64String = reader.result.toString()
            .replace('data:', '')
            .replace(/^.+,/, '');

            console.log(base64String);

            holder=base64String;

        return base64String;

    };

    console.log(file[i].size);
    console.log(file[i].type);
    console.log(file[i].name);
    console.log(holder); */
