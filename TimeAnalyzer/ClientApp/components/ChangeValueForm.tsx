import  * as React  from 'react';
import Chart from './Chart';
import RadioButton from './ChartRadioButton';
import TimeReportApiService from './services/TimeReportApiService'


export default class ChangeValueForm extends React.Component<any,any>{
     timeReport: TimeReportApiService;

     currentTypedValue: any;
     currentTypedDate:any;
    

     constructor(props: any) {
         super(props);
         var data = [{ }]
         this.timeReport = new TimeReportApiService();
         this.state = {
             chartData: data,
             selectedActivityId: 0       
         } 
         this.setNewSelectedActivity = this.setNewSelectedActivity.bind(this);
         this.updateCurrentTypedDate = this.updateCurrentTypedDate.bind(this);
         this.updateCurrentTypedValue = this.updateCurrentTypedValue.bind(this);
     }

     setNewSelectedActivity(ActivityId: any) {
         this.setState({
             selectedActivityId: ActivityId
         });
     }

     updateCurrentTypedDate(e: any) {
         this.setState({
            currentTypedDate : e.currentTarget.value
         })
          
    }

     updateCurrentTypedValue(e: any) {
        this.setState({
            currentTypedValue : e.currentTarget.value
         })
     }

     changeSelectedActivityValue(e: any) {
        debugger;
        var Date = this.currentTypedDate;
        var Duration = this.currentTypedValue;
        var ActivityId = this.state.selectedActivityId;
        return this.timeReport.addTimeReport(Date, Duration, ActivityId).then((response: any) => {
            return Promise.resolve(response);
        });
        //  var selectedTimeReport = this.state.chartData
        //      .filter((dataObject: any) => dataObject.Activity.Id == this.state.selectedActivityId)[0];
     }

     getActivitiesDurationSumWithoutSelectedActivityId() {
         return this.state.chartData
             .filter((timeReport: any) => timeReport.Activity.Id !== this.state.selectedActivityId)
             .map((timeReport: any) => timeReport.Duration)
             .reduce((accumulator: any, currentValue: any) => accumulator + currentValue)
     }
    
    render(){
        return(
            <div className="formContainer">
                        <form className="form" onSubmit={this.changeSelectedActivityValue}>
                            <div className="choise">

                                <div className="changeValue" style={{background:"#000", color:"#000"}}>
                                    <input name="currentTypedDate" type="date" value={this.state.currentTypedDate || ""} onChange={this.updateCurrentTypedDate}></input>
                                    <input name="currentTypedValue" type="number" value={this.state.currentTypedValue || ""}
                                        onChange={this.updateCurrentTypedValue}
                                        placeholder="type your value"></input>
                                    <button type="submit">Change value</button>
                                </div>

                                {this.props.chartData.map((dataObject: any) => {
                                    return <RadioButton
                                        checked={dataObject.activity.Id == this.state.selectedActivityId}
                                        labelName={dataObject.activity.name}
                                        handleChange={this.setNewSelectedActivity}
                                    />
                                })}
                            </div>

                        </form> 
                    </div>
        )
    }
}