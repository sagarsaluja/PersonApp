import { useRef } from "react";
import Card from "../ui/Card";
import styles from "./Newperson.module.css";

function NewpersonForm(props) {
  //const idRef = useRef();
  const firstnameRef = useRef();
  const lastnameRef = useRef();
  const ageRef = useRef();
  //let hist = useHistory();

  // //makes a reference object
  function submitHandler(event) {
    event.preventDefault();
    const entered_data = {
      firstname: firstnameRef.current.value,
      lastname: lastnameRef.current.value,
      age: ageRef.current.value,
    };
    //console.log(entered_data);

    //validation
    // var isValid = true;
    var error_message = "";
    var V = ValidateData(entered_data, error_message);
    if (!V.isValid) {
      alert(V.error_message);
      return;
    }
    //we want to send entered data to the parent here
    const url = "https://localhost:7132/api/Person";
    fetch(url, {
      method: "POST",
      headers: {
        "content-type": "application/json",
        "Access-Control-Allow-Origin": "*",
      },
      body: JSON.stringify(entered_data),
    })
      .then((response) => {
        //console.log("response", response);
        if (response.status === 201) {
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
  //props.onAddPerson(entered_data);

  return (
    <Card>
      <form className={styles.form} onSubmit={submitHandler}>
        <div className={styles.control}>
          <label htmlFor="first_name">First Name</label>
          <input
            type="text"
            required
            id="first_name"
            ref={firstnameRef}
          ></input>
        </div>
        <div className={styles.control}>
          <label htmlFor="last_name">Last Name</label>
          <input type="text" required id="last_name" ref={lastnameRef}></input>
        </div>
        <div className={styles.control}>
          <label htmlFor="age">Age</label>
          <input type="text" required id="age" ref={ageRef}></input>
        </div>
        <div className={styles.actions}>
          <button type="submit">Add</button>
        </div>
      </form>
    </Card>
  );
}
export default NewpersonForm;
