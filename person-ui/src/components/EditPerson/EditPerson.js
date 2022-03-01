import { useEffect, useState, useRef } from "react";
import Card from "../ui/Card";
import styles from "./Editperson.module.css";
import buttons from "../AllPersons/PersonList.module.css";
function EditPerson(props) {
  //console.log("finally this is called " + props.pass.id);
  const firstnameRef = useRef();
  const lastnameRef = useRef();
  const ageRef = useRef();

  function EditHandler(event) {
    //event.preventDefault();
    const entered_id = props.pass.id;
    const entered_data = {
      id: props.pass.id,
      firstname: firstnameRef.current.value,
      lastname: lastnameRef.current.value,
      age: ageRef.current.value,
    };
    console.log(entered_data);

    //validation of entered_data
    var isValid = true;
    var error_message = "";
    var V = ValidateData(entered_data, error_message);
    if (!V.isValid) {
      alert(V.error_message);
      return;
    }
    //validation ends

    const url = entered_data.id
      ? "https://localhost:7132/api/Person/" + entered_id
      : "https://localhost:7132/api/Person/";
    fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*",
      },
      body: JSON.stringify(entered_data),
    })
      .then((response) => {
        //console.log("response", response);
        if (response.status === 200) {
          alert("success");
        }
      })
      .catch((e) => {
        console.log("error", e);
      });
  }
  function ValidateData(data, isValid, error_message) {
    var obj = {
      isValid: true,
      error_message: "",
    };
    if (data.firstname.length < 2) {
      obj.isValid = false;
      obj.error_message = "First Name cannot be less than 2 characters";
      return obj;
    } else if (data.lastname.length === 0) {
      obj.isValid = false;
      obj.error_message = "Last Name cannot be empty";
      return obj;
    } else if (data.age < 18 || data.age > 60) {
      obj.isValid = false;
      obj.error_message = "Age cannot be less than 18 or greater than 60";
      return obj;
    }
    return obj;
  }
  return (
    <div>
      <h2>Edit Person</h2>

      <Card>
        <form className={styles.form} onSubmit={EditHandler}>
          <div className={styles.control}>
            <label htmlFor="first_name">First Name:</label>
            <input
              type="text"
              required
              id="first_name"
              ref={firstnameRef}
              placeholder={props.pass.firstName}
            ></input>
          </div>
          <div className={styles.control}>
            <label htmlFor="last_name">Last Name: </label>
            <input
              placeholder={props.pass.lastName}
              type="text"
              required
              id="last_name"
              ref={lastnameRef}
            ></input>
          </div>
          <div className={styles.control}>
            <label htmlFor="age">Age:</label>
            <input
              type="text"
              required
              id="age"
              ref={ageRef}
              placeholder={props.pass.age}
            ></input>
          </div>

          <button className={buttons.btn} type="submit">
            Update
          </button>

          <button className={buttons.btn} onClick={props.onCancel}>
            Cancel
          </button>
        </form>
      </Card>
    </div>
  );
}
export default EditPerson;
