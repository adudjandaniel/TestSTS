import { Injectable } from "@angular/core";
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from "rxjs";

export interface IAuthenticationResponse {
    openId: string
}

export interface ICredentials {
    Username: string;
    Password: string;
    ReturnUrl: string;
}

@Injectable({
    providedIn: 'root'
})
export class AuthorizeService {

    constructor(private httpClient: HttpClient) {
    }

    public signIn(signInCredentials: ICredentials): Observable<any>
    {
        const url = '/account/login';
        return this.httpClient.post<any>(url,
            signInCredentials);
    }

    public signOut(logoutId: string): Observable<any>
    {
        var signOutRequest = {};
        const url = '/account/logout';

        return this.httpClient.get<any>(`${url}?logoutId=${logoutId}`);
    }

    public clientName(returnUrl: string): Observable<any> {
        const url = '/account/client';

        var credentials: ICredentials = {
            Username: '',
            Password: '',
            ReturnUrl: returnUrl
        }

        return this.httpClient.post<any>(url, credentials);
    }
}
