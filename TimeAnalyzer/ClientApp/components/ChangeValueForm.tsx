import * as React from 'react';
import Chart from './Chart';
import RadioButton from './ChartRadioButton';
import TimeReportApiService from './services/TimeReportApiService';
import ActivitiesService from './services/ActivitiesService';
import TimeConverterService from './services/TimeConverterService';


export default class ChangeValueForm extends React.Component<any, any>{
    timeReport: TimeReportApiService;
    ActivitiesService: ActivitiesService;
    TimeConverterService: TimeConverterService;

    currentTypedValue: any;
    currentTypedDate: any;


    constructor(props: any) {
        super(props);
        this.timeReport = new TimeReportApiService();

        this.state = {
            reportId: null,
            activities: [],
            date: new Date(),
            minutes: 60,
            selectedActivityId: -1
        };


        this.ActivitiesService = new ActivitiesService();
        this.TimeConverterService = new TimeConverterService();

        this.updateRepotrActivity = this.updateRepotrActivity.bind(this);
        this.updateReportDate = this.updateReportDate.bind(this);
        this.updateReportMinutes = this.updateReportMinutes.bind(this);
        this.onSumbit = this.onSumbit.bind(this);
    }

    initializeActivities() {
        this.ActivitiesService.getAllActivities()
            .then((res: any) => {
                if (this.state.selectedActivityId == -1)
                    var activityId = res.data[0].id;
                else
                    var activityId = this.state.selectedActivityId;

                this.setState({
                    selectedActivityId: activityId,
                    activities: res.data
                });
            })
    }

    componentDidMount() {
        this.initializeActivities();
    }

    componentWillReceiveProps(props: any)
    {
        debugger;
        if(props.selectedReport!=null)
        {
            var date = this.TimeConverterService.fromServerDate(props.selectedReport.date); 
            this.setState({
                reportId: props.selectedReport.id,
                date: date,
                minutes: props.selectedReport.duration,
                selectedActivityId: props.selectedReport.activityId
            });
        }
        else 
        {
            this.setState(this.getDefaultState(this.props.selectedDate));
        }
    }

    getDefaultState(date: any) {
        return {
            reportId: null,
            date: date,
            minutes: 60,
            selectedActivityId: -1
        }
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
        var date = new Date(this.state.date);
        var duration = this.state.minutes;
        var activityId = this.state.selectedActivityId;
        if(this.state.reportId > 0)
        {
            var reportId = this.state.reportId;
            this.timeReport.updateTimeReport(reportId,date, duration, activityId).then((response: any) => {
                this.props.onSubmit(date);
            });
        }
        else
        {
            this.timeReport.addTimeReport(date, duration, activityId).then((response: any) => {
                this.props.onSubmit(date);
            });
        }
    }

    getHtmlFormatDate(date: any) {
        return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
    }

    getHeader()
    {
        if(this.state.reportId > 0)
        {
            return "Update report";
        }
        else
        {
            return "Add new report";
        }
    }

    render() {
        return (
            <div className="windowNuts">
                <h2 className="text-center">{this.getHeader()}</h2>
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
                    <button className="form-control btn btn-success" type="submit">Add</button>
                </form>
            </div>
        )
    }
}