import * as React from 'react';
import Chart from './Chart';
import RadioButton from './ChartRadioButton';
import TimeReportApiService from './services/TimeReportApiService';
import ActivitiesService from './services/ActivitiesService';
import TimeConverterService from './services/TimeConverterService';
import SuggestionsService from "./services/SuggestionsService";


export default class Suggestions extends React.Component<any, any>{

    suggestionsService: SuggestionsService;
    timeConvertorService: TimeConverterService;

    currentTypedValue: any;
    currentTypedDate: any;


    constructor(props: any) {
        super(props);

        this.state = {
            date: new Date(),
            allTypes: [],
            selectedType: 'Loading',
            suggestedActivities: [],
            allSuggestions: []
        };

        this.suggestionsService = new SuggestionsService();
        this.timeConvertorService = new TimeConverterService();

        this.updateSelectedType = this.updateSelectedType.bind(this);
        this.initializeSuggestions = this.initializeSuggestions.bind(this);
    }

    componentDidMount() {
        this.initializeSuggestions()
    }

    componentWillReceiveProps(props: any)
    {
        if(props.selectedDate)
        {
            this.setState({
                date: props.selectedDate
            });
        }
    }

    initializeSuggestions() {
        this.setState({
            allTypes: [],
            selectedType: 'Loading',
            suggestedActivities: [],
            allSuggestions: []
        });
        this.suggestionsService.getAllSuggestions(this.state.date)
            .then((res: any) => {
                var allTypes = res.data.map((kv: any) => kv.activityType);
                var selectedType = allTypes[0];
                var suggestedActivities = res.data.filter((kv: any) => kv.activityType.id === selectedType.id)[0].activities;
                var allSuggestions = res.data;
                this.setState({
                    allTypes: allTypes,
                    selectedType: selectedType,
                    suggestedActivities: suggestedActivities,
                    allSuggestions: allSuggestions
                });
            });
    }

    updateSelectedType(event: any) {
        var newTypeId = +event.target.value;
        var newSelectedType = this.state.allTypes.filter((a: any) => a.id === newTypeId)[0];
        var newActivitiesSuggestions = this.state.allSuggestions.filter((kv: any) => kv.activityType.id === newTypeId)[0].activities;

        this.setState({
            selectedType: newSelectedType,
            suggestedActivities: newActivitiesSuggestions
        });
    }

    render() {
        if (this.state.allTypes.length === 0) {
            return (
                <div className="suggestionsBlock">
                    <h3>Loading suggestions...</h3>
                </div>
            );
        }
        else {
            return (
                <div className="suggestionsBlock">
                    <h1 className="text-center">Suggestions</h1>
                    <select className="form-control" value={this.state.selectedType.id} onChange={this.updateSelectedType}>
                        {this.state.allTypes.map((a: any) =>
                            <option value={a.id} key={a.id}>
                                {a.name}
                            </option>
                        )}
                    </select>
                    <ol>
                        {this.state.suggestedActivities.map((a: any) => {
                            return (<h3 key={a.id}><li>{a.name}</li></h3>);
                        })}
                    </ol>
                    <hr />
                    <button className="btn btn-primary" onClick={this.initializeSuggestions}>
                            Get new suggestions for {this.timeConvertorService.getDatePresentationView(this.state.date)}
                    </button>
                </div>
            );
        }
    }
}