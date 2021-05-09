import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthorizeService } from '../authorize.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor(
    private authorizeService: AuthorizeService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    console.log("Query Params", this.activatedRoute.snapshot.queryParams);
    var logoutId: string = this.activatedRoute.snapshot.queryParams.logoutId;
    
    this.logout(logoutId);
  }

  private logout(logoutId: string): void {
    this.authorizeService.signOut(logoutId).subscribe(response => {
      console.log("Logout Response", response);
      
      if (response.signOutIFrameUrl) {
        var iframe = document.createElement('iframe');
        iframe.width = '0';
        iframe.height = '0';
        iframe.src = response.signOutIFrameUrl;
        document.getElementById('logout-iframe').appendChild(iframe);
      }

      if (response.postLogoutRedirectUri) {
        window.location.href = response.postLogoutRedirectUri;
      } else {
        console.log("You are successfully logged out.");
      }
    });
  }

}
