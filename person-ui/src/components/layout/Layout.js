import MainNavigation from "./MainNavigation";

import { Route, Switch } from "react-router-dom";
import PersonList from "../AllPersons/PersonList";
import Card from "../ui/Card";
import NewpersonForm from "../Newperson/Newperson";
function Layout(props) {
  return (
    <div>
      <MainNavigation>
        <main>{props.children}</main>
      </MainNavigation>
      <Switch>
        <Route path="/" exact={true}>
          <Card>
            <PersonList></PersonList>
          </Card>
        </Route>

        <Route path="/new-person">
          <NewpersonForm></NewpersonForm>
        </Route>
      </Switch>
    </div>
  );
}
export default Layout;
