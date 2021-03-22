import { Injectable } from '@angular/core';
import {RestService} from "./rest.service";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {catchError} from "rxjs/operators";
import {License} from "./models/license";

@Injectable({
  providedIn: 'root'
})
export class LicensesService extends RestService {

  constructor(private httpClient: HttpClient) {
    super();
  }

  getList() : Observable<License[]> {
    let url = `https://localhost:5001/license/list`;
    return this.httpClient.get<License[]>(url)
      .pipe(catchError(this.handleError));
  }

}
