import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';
import { MensajeService } from '../../services/mensaje.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(
    private _router: Router,
    private _loginService: LoginService,
    private _mensajeService: MensajeService
  ) { }

  ngOnInit(): void {
    if (!this._loginService.EstaLogin())
      this._router.navigate(['login']);
  }
}
