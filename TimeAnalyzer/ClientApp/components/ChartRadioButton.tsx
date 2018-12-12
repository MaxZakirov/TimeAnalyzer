import * as React from "react";

const Profile = (props: any) => {
    return (
        <p>
            <input
                className="check"
                type="radio"
                checked={props.checked}
                onChange={() => props.handleChange(props.selectedActivityId)}>
            </input>
            <label>{props.labelName}</label>
        </p>
    );
};

export default Profile;