import { Component } from '@angular/core';
import { NAV_TITLE } from '../../../constants/constants';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  title: string = NAV_TITLE;

  constructor(
    private router: Router
  ){}
  
  logout(){
    localStorage.clear();
    this.router.navigateByUrl("/login");
  }
}
