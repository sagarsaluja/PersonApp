import { useEffect, useState, useRef } from "react";
import styles from "./PersonList.module.css";
import EditModal from "../EditPerson/EditModal";
import DeleteModal from "../DeletePerson/DeleteModal";
import Backdrop from "../Backdrop";
import Wrapper from "../ui/Wrapper";

function PersonList(props) {
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [backDropIsOpen, setBackDropIsOpen] = useState(false);
  const [EditmodalIsOpen, setEditModalIsOpen] = useState(false);
  const [deleteModalIsOpen, setDeleteModalIsOpen] = useState(false);
  const [editID, setEditID] = useState(false);
  const [deleteID, setDeleteID] = useState(false);
  const [confirmDelete, setConfirmDelete] = useState(false);
  const [data, setData] = useState([]);

  const url = "https://localhost:7132/api/Person";
  useEffect(() => {
    fetch(url)
      .then((response) => response.json())
      .then((json) => {
        setData(json);
      })
      .catch((e) => {
        console.log("error", e);
        alert("Person List is Empty");
      });
  }, []);
  function CloseModal() {
    //console.log("close modal is called");
    setEditModalIsOpen(false);
    setDeleteModalIsOpen(false);
    setBackDropIsOpen(false);
  }
  function EditButtonHandler(item) {
    //console.log("editt button handler is called");
    setEditModalIsOpen(true);
    setBackDropIsOpen(true);
    setEditID(item);
    //console.log("edit button pressed");
    // setModalIsOpen(true);
    //console.log(item);
  }
  function onConfirmDelete(item) {
    setConfirmDelete(true);
    CloseModal();
    setData((prevstate) => {
      const filtered = prevstate.filter((value) => {
        return value.id !== item;
      });
      return filtered;
    });
  }
  function DeleteButtonHandler(item) {
    setDeleteModalIsOpen(true);
    setBackDropIsOpen(true);
    setDeleteID(item.id);
    //console.log("delete button pressed");
  }

  return (
    <div>
      <div className={styles.plist}>
        {EditmodalIsOpen ? (
          <EditModal info={editID} onCancel={CloseModal}></EditModal>
        ) : null}
        {deleteModalIsOpen ? (
          <DeleteModal
            info={deleteID}
            onCancel={CloseModal}
            onConfirmDelete={onConfirmDelete}
          ></DeleteModal>
        ) : null}
        {EditmodalIsOpen || deleteModalIsOpen ? (
          <Backdrop BackdropClick={CloseModal} />
        ) : null}
        <h2 className={styles.plistword}>Person List</h2>

        {data.map((item) => {
          return (
            <div className={styles.container}>
              <div id={item.id} key={item.id}>
                <Wrapper>
                  <div>ID:{item.id}</div>
                  <div> First Name:{item.firstName} </div>
                  <div> Last Name:{item.lastName} </div>
                  <div> Age:{item.age} </div>

                  <button
                    className={styles.btn}
                    onClick={() => EditButtonHandler(item)}
                  >
                    Edit
                  </button>
                  <button
                    className={styles.btn}
                    onClick={() => DeleteButtonHandler(item)}
                  >
                    Delete
                  </button>
                </Wrapper>
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}
export default PersonList;
