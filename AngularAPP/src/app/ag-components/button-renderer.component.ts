// Author: T4professor

import { Component } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';

@Component({
  selector: 'app-button-renderer',
  template: `
    <button type="button" class="{{class}}" (click)="onClick($event)">{{label}}</button>
    `
})

export class ButtonRendererComponent implements ICellRendererAngularComp {

  params;
  label: string;
  class: string;

  agInit(params): void {
    this.params = params;
    this.label = this.params.label || null;
    this.class=this.params.class || 'action-button'
  }

  refresh(params?: any): boolean {
    return true;
  }

  onClick($event) {
    if (this.params.onClick instanceof Function) {
      // put anything into params u want pass into parents component
      
      this.params.onClick(this.params.node.data);

    }
  }
}
