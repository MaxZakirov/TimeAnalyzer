import * as React from "react";

const TimeRoller = (props: any) => {
    return (
        <div className="mainDateBlock">
            <button className="btn-block dayRollerBtn" onClick={props.rollBack}></button>
            <h3 className="text-center date">{props.dateString}</h3>
            <button className="btn-block dayRollerBtn" onClick={props.rollForward}></button>
        </div>
    );
};

export default TimeRoller;