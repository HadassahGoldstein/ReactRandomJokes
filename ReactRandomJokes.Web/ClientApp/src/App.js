import React from 'react';
import { Route } from 'react-router';
import Layout  from './components/Layout';
import  Home from './pages/Home';
import { AuthContextComponent } from './AuthContext';
import Signup from './pages/Signup';
import Login from './pages/Login';
import Logout from './pages/Logout';
import ViewAll from './pages/ViewAll';

function App(){
  return(
<AuthContextComponent>
  <Layout>
    <Route exact path='/' component={Home}/>
    <Route exact path='/Signup' component={Signup}/>
    <Route exact path='/login' component={Login}/>
    <Route exact path='/logout' component={Logout}/>
    <Route exact path='/ViewAll' component={ViewAll}/>
  </Layout>
</AuthContextComponent>

  )
}
export default App;

