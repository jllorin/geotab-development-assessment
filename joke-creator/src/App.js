import React from 'react';
import './App.css';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import Paper from '@material-ui/core/Paper';
import styled from 'styled-components';
import TextField from '@material-ui/core/TextField';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { getCategories, getJokes, getPerson } from './api';
import Switch from '@material-ui/core/Switch';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import Divider from '@material-ui/core/Divider';
import ListItemText from '@material-ui/core/ListItemText';

const AppRoot = styled.div `  
  display: flex;
  justify-content: center;
  padding: 20px;
  > div {
    width: 50%;
  }
  form {
    padding: 40px;
    height: 100%;
    > div {
      margin-bottom: 16px;
      display: flex;
      align-items: center;
    }
    .line {      
      > div {
        width: 50%;
        margin-right: 20px;
      }
      > div:last-child {
        margin-right: 0px;
      }
    }
    .random-name-label {
      padding-right: 30px;
    }
    > div:last-child {
      justify-content: flex-end;
      button {
        text-align: right;
      }
    }
  }
  .joke-details {
    display: flex;
    justify-content: center;
    padding: 0 40px 40px 40px;
    > div {
      width: 90%;
    }
    /* width: 50%; */
  }
`

const initialState = {
  category: '',
  noOfJokes: 1,
  randomNameUsed: false,
  firstName: '',
  lastName: '',
  jokes: []
}

function App() 
{
  const [options, setOptions] = React.useState(initialState);  
  const [categories, setCategories] = React.useState([]);
  const [errors, setErrors] = React.useState([]);

  React.useEffect(() => {
    async function fetchData() {
      const pulledCategories = await getCategories();
      if (pulledCategories.length > 0) {
        setCategories(pulledCategories)
      }
   }
   fetchData();
  }, []);

  const onChangeRandomName = async (e) => {
    const randomNameUsed = e.target.checked;    
    if (randomNameUsed) {
      const person = await getPerson();
      setOptions({...options, firstName: person.firstName, lastName: person.lastName, randomNameUsed: randomNameUsed});
    }
    else {
      setOptions({...options, firstName: '', lastName: '', randomNameUsed: randomNameUsed});
    }    
  }

  const onNoOfJokesChanged = (e) => {
    setOptions({...options, noOfJokes: e.target.value});
  }

  const onSubmit = async (e) => {
    e.preventDefault();
    let errors = [];
    const noOfJokes = parseInt(options.noOfJokes);
    if (noOfJokes < 1 || noOfJokes > 9) {      
      errors.push({key: 'noOfJokes', message: 'Number of jokes is out of range.'});      
    }
    if (errors.length > 0) {
      setErrors(errors);
      setOptions({...options, jokes: []});
      return;
    }
    else {
      setErrors([]);
    }
    
    const payload = {
      category: options.category,
      noOfJokes: noOfJokes,
      replaceFirstNameWith: options.firstName,
      replaceLastNameWith: options.lastName
    }
    
    const jokes = await getJokes(payload);
    setOptions({...options, jokes: jokes});
  }

  const onCategoryChange = (event, newValue) => {    
    setOptions({...options, category: newValue ? newValue : ''});
  }

  return (
    <AppRoot>
      <div>
        <AppBar position='static'>
          <Toolbar>
            <IconButton edge='start' color='inherit' aria-label='menu'>
              <MenuIcon />
            </IconButton>
            <Typography variant='h6' >
              Joke Generator
            </Typography>
          </Toolbar>
        </AppBar>                
        <Paper elevation={3}>
          <form id='joke-form' onSubmit={onSubmit}>
            <div className='line'>
              <Autocomplete id='combo-box-demo' options={categories} getOptionLabel={(option) => option} 
                  onChange={onCategoryChange} renderInput={(params) => 
                    <TextField {...params} label='Enter a category' variant='outlined' />} />
              <TextField id='outlined-number' required label='How many jokes do you want? (1-9)' type='number' variant='outlined' 
                value={options.noOfJokes} onChange={onNoOfJokesChanged} 
                error={errors.find(error => error.key === 'noOfJokes') !== undefined}
                helperText={errors.find(error => error.key === 'noOfJokes')?.message} />              
            </div>
            <div>
              <Typography className='random-name-label' variant='subtitle1'>Want to use a random name?</Typography>
              <Typography variant='subtitle1'>No</Typography>
              <Switch color="primary" name="randomNameChecked" onChange={onChangeRandomName} />
              <Typography variant='subtitle1'>Yes</Typography>
            </div>
            {options.randomNameUsed && <div className='line'>
              <TextField id='outlined-number' label='Replace Chuck with?' variant='outlined' value={options.firstName}
                InputProps={{ readOnly: true, }} InputLabelProps={{ shrink: true }} />              
              <TextField id='outlined-number' label='Replace Norris with?' variant='outlined' value={options.lastName}
                InputProps={{ readOnly: true }} InputLabelProps={{ shrink: true }} />
            </div>}
            <div>
              <Button variant="contained" color="primary" type="submit">Submit</Button>
            </div>
          </form>
          {options.jokes.length > 0 && <div className='joke-details'>
            <Paper elevation={2}>
              <List>
                {
                  options.jokes.map((joke, index) => <React.Fragment key={index}>
                    <ListItem alignItems="flex-start">
                      <ListItemText
                        primary={`Joke #${index+1}`}
                        secondary={
                          <React.Fragment>
                            <Typography component="span" variant="body2" color="textPrimary">
                              {joke}
                            </Typography>
                            {/* {`${joke}`} */}
                          </React.Fragment>
                        }
                      />
                    </ListItem>
                    <Divider light />
                  </React.Fragment>)
                }
              </List>          
            </Paper>
          </div>}
        </Paper>
      </div>        
    </AppRoot>
  );
}

export default App;
