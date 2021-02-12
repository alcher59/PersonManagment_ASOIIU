import { Component, OnInit, ViewChild } from '@angular/core';
import { GanttEditorComponent, GanttEditorOptions } from 'ng-gantt';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss']
})
export class ScheduleComponent implements OnInit {

  @ViewChild("editor") editor: GanttEditorComponent;
  public editorOptions: GanttEditorOptions;
  public data: any;
  constructor() { }

  ngOnInit(): void {
    this.data = this.initialData();
    //https://jsganttimproved.github.io/jsgantt-improved/docs/index.js
    this.editorOptions = {
      vFormat: "day",
      vEditable: false,
      vShowRes: false,
      vShowCost: false,
      vShowAddEntries: false,
      vShowComp: false,
      vShowDur: false,
      vShowStartDate: false,
      vShowEndDate: false,
      vShowPlanStartDate: false,
      vShowPlanEndDate: false,
      vLang: 'ru',
      vEventsChange: {
        taskname: () => {
          console.log("taskname");
        }
      }
    };
  }

  initialData() {
    return [
      {
        pID: 1,
        pName: "АЛЕКСАНДРОВА ЕЛЕНА СВЯТОСЛАВОВНА",
        pStart: "2021-07-19",
        pEnd: "2021-08-01",
        pClass: "gtaskblue",
        pLink: "",
        pMile: 0,
        pRes: "Отпуск",
        pComp: 0,
        pGroup: 0,
        pParent: 0,
        pOpen: 1,
        pDepend: "",
        pCaption: "",
        pNotes: ""
      },
      {
        pID: 2,
        pName: "АКУЛОВ СЕМЕН АЛЕКСЕЕВИЧ",
        pStart: "2021-05-31",
        pEnd: "2021-06-13",
        pClass: "gtaskblue",
        pLink: "",
        pMile: 0,
        pRes: "Отпуск",
        pComp: 0,
        pGroup: 0,
        pParent: 0,
        pOpen: 1,
        pDepend: "",
        pCaption: "",
        pNotes: ""
      },
      {
        pID: 3,
        pName: "АНУФРИЕВ ДМИТРИЙ ВИТАЛЬЕВИЧ",
        pStart: "2021-08-10",
        pEnd: "2021-08-23",
        pClass: "gtaskblue",
        pLink: "",
        pMile: 0,
        pRes: "Отпуск",
        pComp: 0,
        pGroup: 0,
        pParent: 0,
        pOpen: 1,
        pDepend: "",
        pCaption: "",
        pNotes: ""
      },
      {
        pID: 4,
        pName: "БАЛДИНА ТАТЬЯНА РЭМОВНА",
        pStart: "2021-07-19",
        pEnd: "2021-07-30",
        pClass: "gtaskblue",
        pLink: "",
        pMile: 0,
        pRes: "Отпуск",
        pComp: 0,
        pGroup: 0,
        pParent: 0,
        pOpen: 1,
        pDepend: "",
        pCaption: "",
        pNotes: ""
      }
    ];
  }
}
