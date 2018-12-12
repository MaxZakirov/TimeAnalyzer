import * as React from "react";
import { Pie, Doughnut, Radar, Polar } from 'react-chartjs-2';
import Profile from './ChartRadioButton';
import TimeReportApiService from './TimeReportApiService'


export default class Chart extends React.Component<any, any> {

    Report: TimeReportApiService;

    currentTypedValue: any;

    constructor(props: any) {
        super(props);
        var initialChartData = [{}];
        this.state = {
            fields: {},
            chartData: initialChartData,
            selectedActivityId: 0,
            selectedOption: 'mops'
        }
        this.Report = new TimeReportApiService();
        this.currentTypedValue = 0;
    }

    initializeChartData(): any {
        this.Report.getTimeReports(this.state.Id, this.state.Date, this.state.Duration, this.state.ActivityId, this.state.Activity)
            .then((res: any) => {
                res.unshift(this.state.chartData);
                this.setState({
                    chartData: res,
                    selectedActivityId: res[0].Activity.Id
                });
            });
    }

    setNewSelectedActivity(activityId: any) {
        this.setState({
            selectedActivityId: activityId
        });
    }

    updateCurrentTypedValue(e: any) {
        this.currentTypedValue = e.target.value;
    }

    changeSelectedActivityValue(e: any) {
        var selectedTimeReport = this.state.chartData
            .filter((dataObject: any) => dataObject.Activity.Id == this.state.selectedActivityId)[0];
    }

    validateNewActivityTimeValue(value: any) {
        var leftMinutes = 1440 - this.getActivitiesDurationSumWithoutSelectedActivityId() - value;
        return leftMinutes >= 0;
    }

    getActivitiesDurationSumWithoutSelectedActivityId() {
        return this.state.chartData
            .filter((timeReport: any) => timeReport.Activity.Id !== this.state.selectedActivityId)
            .map((timeReport: any) => timeReport.Duration)
            .reduce((accumulator: any, currentValue: any) => accumulator + currentValue)
    }

    render() {
        return (
            <div className="mainPage">

                 <div className="center">
                    <div className="chartContainer">
                        <div className="chart">
                            <Doughnut
                                data={{
                                    labels: this.state.chartData.map((dataObject: any) => dataObject.Activity.Name),
                                    datasets: [{
                                        label: 'Power',
                                        data: this.state.chartData.map((dataObject: any) => dataObject.Duration),
                                        backgroundColor: this.state.chartData.map((dataObject: any) => dataObject.Activity.ColorValue),
                                        borderWidth: 2,
                                        borderColor: '#fff',
                                        hoverBorderWidth: 2,
                                        hoverBorderColor: '#eee'
                                    }]
                                }}
                                options={{
                                    legend: {
                                        display: true,
                                        position: 'left',
                                        labels: {
                                            fontSize: 15,
                                            fontColor: '#eee'
                                        }
                                    }
                                }}
                            />
                        </div>
                    </div>

                    <div className="formContainer">
                        <form className="form" onSubmit={this.changeSelectedActivityValue}>
                            <div className="choise">

                                <div className="changeValue">
                                    <input type="number" value={this.currentTypedValue}
                                        onChange={this.updateCurrentTypedValue}
                                        placeholder="type your value"></input>
                                    <button type="submit">Change value</button>
                                </div>

                                {this.state.chartData.map((dataObject: any) => {

                                    return <Profile
                                        checked={dataObject.Activity.Id == this.state.selectedActivityId}
                                        labelName={dataObject.Activity.Name}
                                        handleChange={this.setNewSelectedActivity}
                                    />
                                })}
                            </div>

                        </form>
                    </div>
                </div> 
            </div>
        )
    }
}