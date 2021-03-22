import {Component, OnInit} from '@angular/core';
import {License, LicenseListModel, LicenseToCreate} from "./services/models/license";
import {HttpClient} from "@angular/common/http";
import {LicensesService} from "./services/licenses.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public isCreate: boolean;
  public name: string;
  public licenseToCreate: LicenseToCreate;
  public licenseList: LicenseListModel[] = [];
  public response: {dbPath: ''};

  constructor(private http: HttpClient,  private licenseServcie: LicensesService){}

  ngOnInit(){
    this.isCreate = true;
    this.getUsers();
  }

  public onCreate = () => {
    this.licenseToCreate = {
      name: this.name,
      filePath: this.response.dbPath
    }

    this.http.post('https://localhost:5001/license/add', this.licenseToCreate)
      .subscribe(res => {
        this.getUsers();
        this.isCreate = false;
      });
  }

  private getUsers = () => {
    this.licenseServcie.getList()
      .subscribe(res => {
        this.licenseList = res as LicenseListModel[];
      });
  }

  public uploadFinished = (event) => {
    this.response = event;
  }

}
