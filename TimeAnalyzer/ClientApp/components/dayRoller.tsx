import * as React from "react";

const DayRoller = (props: any) => {
    return (
        <div className="col-sm-3 mainDateBlock">
            <button className="btn-block dayRollerBtn" onClick={props.rollBack}></button>
            <h3 className="text-center date">{props.dateString}</h3>
            <button className="btn-block dayRollerBtn" onClick={props.rollForward}></button>
        </div>
    );
};

export default DayRoller;