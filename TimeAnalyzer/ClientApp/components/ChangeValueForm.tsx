import * as React from 'react';
import Chart from './Chart';
import RadioButton from './ChartRadioButton';
import TimeReportApiService from './services/TimeReportApiService';
import ActivitiesService from './services/ActivitiesService';


export default class ChangeValueForm extends React.Component<any, any>{
    timeReport: TimeReportApiService;
    ActivitiesService: ActivitiesService;

    currentTypedValue: any;
    currentTypedDate: any;


    constructor(props: any) {
        super(props);
        var data = [{}]
        this.timeReport = new TimeReportApiService();
        this.state = {
            activities: [],
            date: new Date(),
            minutes: 60,
            selectedActivityId: 0
        }
        this.ActivitiesService = new ActivitiesService();

        this.updateRepotrActivity = this.updateRepotrActivity.bind(this);
        this.updateReportDate = this.updateReportDate.bind(this);
        this.updateReportMinutes = this.updateReportMinutes.bind(this);
        this.onSumbit = this.onSumbit.bind(this);
    }

    initializeActivities() {
        this.ActivitiesService.getAllActivities()
            .then((res: any) => {
                this.setState({
                    selectedActivityId: res.data[0].id,
                    activities: res.data
                });
            })
    }

    componentDidMount() {
        this.initializeActivities();
    }

    updateRepotrActivity(activityId: any) {
        this.setState({
            selectedActivityId: activityId
        });
    }

    updateReportDate(e: any) {
        var date = new Date(e.currentTarget.value);
        this.setState({
            date: date
        })
    }

    updateReportMinutes(e: any) {
        this.setState({
            minutes: +e.currentTarget.value
        });

    }

    onSumbit(e: any) {
        e.preventDefault();
        debugger;
        var date = new Date(this.state.date);
        var duration = this.state.minutes;
        var activityId = this.state.selectedActivityId;
        return this.timeReport.addTimeReport(date, duration, activityId).then((response: any) => {
            alert('success!');
        }, (err: any) => {
            console.log(err);
        });
    }

    getHtmlFormatDate(date: any) {
        return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
    }

    render() {
        return (
            <div className="windowNuts">
                <h2 className="text-center">Add new report</h2>
                <form onSubmit={this.onSumbit}>
                    <div>
                        <div>
                            <div className="form-group">
                                <label htmlFor="ReportDate">Date</label>
                                <input className="form-control" name="reportDate"
                                    type="date" value={this.getHtmlFormatDate(this.state.date)} onChange={this.updateReportDate} />
                            </div>
                            <div className="form-group">
                                <label htmlFor="reportMinutes">Minutes</label>
                                <input className="form-control" name="reportMinutes" type="number" value={this.state.minutes}
                                    onChange={this.updateReportMinutes}
                                    placeholder="type your value"></input>
                            </div>
                        </div>
                        {this.state.activities.map((activity: any) => {
                            return <RadioButton
                                key={activity.id}
                                checked={activity.id == this.state.selectedActivityId}
                                labelName={activity.name}
                                handleChange={this.updateRepotrActivity}
                                id={activity.id}
                            />
                        })}
                    </div>
                    <button className="form-control btn btn-success" type="submit">Add </button>
                </form>
            </div>
        )
    }
}