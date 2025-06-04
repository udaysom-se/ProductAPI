import { Routes } from '@angular/router';
import { ProductComponent } from './components/product/product.component'; // Update the path accordingly

export const routes: Routes = [
    { path: '', redirectTo: '/products', pathMatch: 'full' },
    { path: 'products', component: ProductComponent }
];
//something