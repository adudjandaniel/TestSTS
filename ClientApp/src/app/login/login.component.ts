import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthorizeService } from '../authorize.service';

export const ReturnUrlType = 'ReturnUrl';

interface INavigationState {
  [ReturnUrlType]: string;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  clientName: string;

  constructor(
    private authorizeService: AuthorizeService,
    private activatedRoute: ActivatedRoute) {
      this.authorizeService.clientName(this.getReturnUrl()).subscribe(
        response => this.clientName = response.clientName,
        error => this.clientName = 'Test STS');
  }

  ngOnInit() {
  }

  public login(email: string, password: string): void {
    const credentials = {
      Username: email,
      Password: password,
      ReturnUrl: this.getReturnUrl()
    };

    this.authorizeService.signIn(credentials).subscribe(response => {
      window.location.href = response.redirectUrl;
    })
  }

  private getReturnUrl(): string {
    const fromQuery = 
      (this.activatedRoute.snapshot.queryParams as INavigationState).ReturnUrl;

    return fromQuery;
  }
}
