import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  template: `
    <h2 mat-dialog-title>Potwierdź</h2>
    <div mat-dialog-content>{{ data }}</div>
    <div mat-dialog-actions>
      <button mat-button (click)="onClick(true)">Tak</button>
      <button mat-button (click)="onClick(false)">Nie</button>
    </div>
  `,
})
export class ConfirmDialogComponent {
    constructor(
      @Inject(MAT_DIALOG_DATA) public data: string,
      public dialogRef: MatDialogRef<ConfirmDialogComponent>
    ) {}
  
    onClick(result: boolean): void {
      this.dialogRef.close(result);
    }
  }
