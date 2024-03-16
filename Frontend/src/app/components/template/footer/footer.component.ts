import { Component } from '@angular/core';
import { NAV_TITLE } from '../../../constants/constants';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css'
})
export class FooterComponent {
  company: string = NAV_TITLE;
}
