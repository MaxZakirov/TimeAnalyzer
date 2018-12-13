import * as React from "react";
import { Pie, Doughnut, Radar, Polar } from 'react-chartjs-2';
import Profile from './ChartRadioButton';
import TimeReportApiService from './services/TimeReportApiService'
import ChartService from './services/ChartService';

export default class Chart extends React.Component<any, any> {

    ReportsApi: TimeReportApiService;
    ChartService: ChartService;

    constructor(props: any) {
        super(props);
        var initialChartData: any[] = [];
        this.state = {
            chartData: initialChartData,
            selectedDate: new Date()
        }

        this.ChartService = new ChartService();
        this.ReportsApi = new TimeReportApiService();
    }

    initializeChartData(): any {
        this.ReportsApi.getDayUserTimeReports(this.state.selectedDate)
            .then((res: any) => {
                this.setState({
                    chartData: res.data
                });
            });
    }

    componentDidMount() {
        this.initializeChartData();
    }

    getChartData(): any {
        var reports = this.ChartService.fillEmptyPartOfDay(this.state.chartData);
        console.log(reports);
        var chartValues = reports.map((dataObject: any) => dataObject.duration);
        var chartLabels = reports.map((dataObject: any) => dataObject.activity.name);
        var chartColors = reports.map((dataObject: any) => dataObject.activity.colorValue);
        debugger;

        return {
            labels: chartLabels,
            datasets: [{
                label: 'Power',
                data: chartValues,
                backgroundColor: chartColors,
                borderWidth: 2,
                borderColor: '#fff',
                hoverBorderWidth: 2,
                hoverBorderColor: '#eee'
            }]
        };
    }

    getChartOptions(): any {
        return {
            legend: {
                display: true,
                position: 'left',
                labels: {
                    fontSize: 15,
                    fontColor: '#eee'
                }
            }
        };
    }

    render() {
        return (
            <div className="mainPage">
                <div className="container">
                    <h3 className="text-left mainDate">13 DECEMBER</h3>
                    <Doughnut
                        data={this.getChartData()}
                        options={this.getChartOptions()}
                    />
                </div>
            </div>
        )
    }
}