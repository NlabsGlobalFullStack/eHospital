import { Routes } from '@angular/router';
import { LayoutComponent } from './components/template/layout/layout.component';
import { HomeComponent } from './components/home/home.component';

export const routes: Routes = [
    {
        path: "",
        component: LayoutComponent,
        children:[
            {
                path: "",
                component: HomeComponent
            }
        ]
    }
];
