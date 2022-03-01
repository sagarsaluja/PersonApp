import buttons from "../AllPersons/PersonList.module.css";
function DeletePerson(props) {
  //console.log(props.pass + "finally reached here?");
  const delete_event = () => {
    // const data = {
    //   id: "6d972e26-72c7-4737-a6a8-c69ef0bafa81",
    // };
    const url = "https://localhost:7132/api/Person/" + props.pass;

    fetch(url, {
      method: "DELETE",
      headers: {
        "content-type": "application/json",
        "Access-Control-Allow-Origin": "*",
      },
      //body: JSON.stringify(data),
    })
      .then((response) => {
        //console.log("response", response);
        if (response.status === 204) {
          alert("success");
          //mistake , don't use get element by id -> javascript thing not a react thing

          props.onConfirmDelete(props.pass);
          // props.setDeletedState();
          //console.log("complete");
        }
      })
      .catch((e) => {
        console.log("error", e);
      });
  };

  return (
    <div>
      <h2>Are you sure?</h2>

      <button className={buttons.btn} type="submit" onClick={delete_event}>
        YES
      </button>
      <button className={buttons.btn} onClick={props.onCancel}>
        No
      </button>
    </div>
  );
}
export default DeletePerson;
