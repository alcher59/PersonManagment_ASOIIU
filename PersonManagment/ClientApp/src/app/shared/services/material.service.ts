import { ElementRef } from '@angular/core';

declare var M;

export interface MaterialInstance {
    open?(): void;
    close?(): void;
    destroy?(): void;
}

export interface MaterialDatepicker extends MaterialInstance {
    date?: Date;
}

export interface MaterialSelect extends MaterialInstance {
    selected: any;
}

export class MaterialService {
    static toast(message: string) {
        M.toast({html: message})
    }

    static initFloatingButton(ref: ElementRef) {
        M.FloatingActionButton.init(ref.nativeElement);
    }

    static updateTextInputs() {
        M.updateTextFields();
    }

    static initModal(ref: ElementRef): MaterialInstance {
        return M.Modal.init(ref.nativeElement);
    }

    static initTooltip(ref: ElementRef): MaterialInstance {
        return M.Tooltip.init(ref.nativeElement);
    }

    static initDatepicker(ref: ElementRef, onClose: () => void): MaterialDatepicker {
        return M.Datepicker.init(ref.nativeElement, { 
            format: 'dd.mm.yyyy',
            showClearBtn: true,
            minDate: new Date(1960, 1, 1),
            onClose: onClose
        });
    }

    static initTapTarget(ref: ElementRef): MaterialInstance {
        return M.TapTarget.init(ref.nativeElement);
    }

    static initSelect(ref: ElementRef): MaterialSelect {
        const options = {

        };
        return M.FormSelect.init(ref.nativeElement, options);
    }
}