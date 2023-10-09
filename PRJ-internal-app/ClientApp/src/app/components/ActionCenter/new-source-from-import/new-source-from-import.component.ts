import { Component, OnInit } from '@angular/core';
import { faFolderOpen, faAddressCard, faHistory } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-new-source-from-import',
  templateUrl: './new-source-from-import.component.html',
  styleUrls: ['./new-source-from-import.component.css']
})
export class NewSourceFromImportComponent implements OnInit {
  faFolderOpen = faFolderOpen;
  faAddressCard = faAddressCard;
  faHistory = faHistory;

  constructor() { }

  ngOnInit(): void {
  }

}
