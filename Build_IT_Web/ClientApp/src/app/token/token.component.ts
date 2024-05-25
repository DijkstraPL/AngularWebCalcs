import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from '@main/api-authorization/authorize.service';

@Component({
  selector: 'app-token',
  templateUrl: './token.component.html',
  styleUrls: ['./token.component.css']
})
export class TokenComponent implements OnInit {
  _token: string = "";
  _isError: boolean = false;
  _isCopied: boolean = false;

  constructor(private readonly authorizeService: AuthorizeService) {}

  ngOnInit(): void {
    this._isCopied = false;
    this.authorizeService.getAccessToken().subscribe(
      (t) => {
        this._token = "Bearer " + t;
        this._isError = false;
      },
      (err) => {
        this._isError = true;
      }
    );
  }

  copyToClipboard(): void {
    const selBox = document.createElement("textarea");
    selBox.style.position = "fixed";
    selBox.style.left = "0";
    selBox.style.top = "0";
    selBox.style.opacity = "0";
    selBox.value = this._token;
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand("copy");
    document.body.removeChild(selBox);
    this._isCopied = true;
  }

}
