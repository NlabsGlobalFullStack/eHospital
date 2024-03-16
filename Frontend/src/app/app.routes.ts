import { Routes } from '@angular/router';
import { LayoutComponent } from './components/template/layout/layout.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { inject } from '@angular/core';
import { AuthService } from './services/auth.service';
import { PatientComponent } from './components/patient/patient/patient.component';
import { NurseComponent } from './components/nurse/nurse/nurse.component';
import { DoctorComponent } from './components/doctor/doctor/doctor.component';
import { AdminComponent } from './components/admin/admin/admin.component';
import { NullComponent } from './components/null/null.component';
export const routes: Routes = [
    {
        path: "login",
        component: LoginComponent
    },  
    // {
    //     path: "",        //UserType göre yönlendirme işlemini henüz yapamadım burası kaldı
    //     component: HomeComponent
    // },
    {
        path: "",
        component: HomeComponent
    },
    {
        path: "",
        component: LayoutComponent,
        canActivateChild: [() => inject(AuthService).isAuthenticated()],
        children: [
            {
                path: "employee",
                component: HomeComponent
            },
            {
                path: "patient",
                component: PatientComponent
            },
            {
                path: "nurse",
                component: NurseComponent
            },
            {
                path: "doctor",
                component: DoctorComponent
            },
            {
                path: "admin",
                component: AdminComponent
            }
        ]
    }
];
